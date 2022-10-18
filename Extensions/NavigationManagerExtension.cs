using System;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.JSInterop;

public static class NavigationManagerExtensions
{

		// Busca el valor de un parametro del QueryString y si lo obtiene lo pone en la variable que le pases.
		// Si no existe el parametro no actualiza esa variable, queda como estaba.
		// Devuelve true si existe el parametro, o false si no existe.
    public static bool GetQueryStringValue<T>(this NavigationManager navManager, string key, ref T? value)
    {
        var uri = navManager.ToAbsoluteUri(navManager.Uri);

				// Si existe el valor lo parseo, lo pongo en la variable y devuelvo true
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
        {
						// Int
            if ((typeof(T) == typeof(int) || typeof(T) == typeof(int?)) && int.TryParse(valueFromQueryString, out var valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            // String
						if (typeof(T) == typeof(string))
            {
                value = (T)(object)valueFromQueryString.ToString();
                return true;
            }

            // Boolean
						if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
            {
							string str=(string)(object)valueFromQueryString.ToString().ToUpper();

							if (str=="TRUE" || str=="1")
								value = (T)(object)true;
							else
								value=(T)(object)false;

							return true;
            }

						// Decimal
            if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
            {
                value = (T)(object)valueAsDecimal;
                return true;
            }

						// DateTime
            if ((typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?)) && DateTime.TryParse(valueFromQueryString, out var valueAsDate))
            {
                value = (T)(object)valueAsDate;
                return true;
            }
        }

				// Si no encuentro el parametro devuelvo false.
				// La variable de devolucion no es modificada.
        return false;
    }


	// Actualiza el valor de un parametro en el QueryString (sin recargar la pagina).
	// Si el parametro no existe se crea. Si le pasas null se borra.
	// HistoryReplace: Si es true reemplaza la pagina actual en el historial (necesita JSRuntime)
	public static async Task ReplaceQueryStringValue(this NavigationManager navManager, string key, string? value, bool historyReplace = false, Microsoft.JSInterop.IJSRuntime? JSRuntime = null)
	{

		if (historyReplace && JSRuntime==null) {
			throw new Exception("Error al intentar ReplaceQueryStringValue: debe pasar un JSRuntime si desea reemplazar en el historial");
		}

		string newUri = "";

		// Obtengo la Uri actual
		var uri = navManager.ToAbsoluteUri(navManager.Uri);

		// Armo un dictionary key=>value con el querystring actual
		var qsDict=QueryHelpers.ParseQuery(uri.Query);

		// Si el parametro tiene valor null es que hay que borrarlo, saco el parametro del dictionary
		if (value==null)
			qsDict.Remove(key);
		else
			// Si no, actualizo (o creo) el valor en el dictionary
			qsDict[key]=value;

		// Recreo la Uri
		newUri=QueryHelpers.AddQueryString(uri.AbsolutePath,qsDict);

		// Actualizo segun si debo reemplazar la actual en el historial
		if (historyReplace && JSRuntime!=null) {
			// Uso el NavigateTo desde JS porque del lado server todavia no tiene un parametro expuesto para reemplazar en historial
			await JSRuntime.InvokeVoidAsync("Blazor._internal.navigationManager.navigateTo", newUri, false, true);
		}
		else
			navManager.NavigateTo(newUri);

	}


}
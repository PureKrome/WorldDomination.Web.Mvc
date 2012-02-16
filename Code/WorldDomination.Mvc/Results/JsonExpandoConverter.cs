using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;

namespace WorldDomination.Web.Mvc.Results
{

    // NOTE: Code taken from http://www.patridgedev.com/2011/08/24/getting-dynamic-expandoobject-to-serialize-to-json-as-expected/
    //       and http://theburningmonk.com/2011/05/idictionarystring-object-to-expandoobject-extension-method/



    /// <summary>
    /// Allows JSON serialization of Expando objects into expected results (e.g., "x: 1, y: 2") instead of the default dictionary serialization.
    /// </summary>
    public class JsonExpandoConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new[] {typeof (ExpandoObject)}); }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type,
                                           JavaScriptSerializer serializer)
        {
            // See source code link for this extension method at the bottom of this post (/Helpers/IDictionaryExtensions.cs)
            return dictionary.ToExpando();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var dictionary = obj as IDictionary<string, object>;
            if (dictionary == null)
            {
                throw new ArgumentException("obj is not of type Dictionary<string, object>.");
            }

            return dictionary.ToDictionary(item => item.Key, item => item.Value);
        }
    }

    public static class DictionaryExtensions
    {
        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// </summary>
        public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>) expando;

            // go through the items in the dictionary and copy over the key value pairs)
            foreach (var kvp in dictionary)
            {
                // if the value can also be turned into an ExpandoObject, then do it!
                if (kvp.Value is IDictionary<string, object>)
                {
                    var expandoValue = ((IDictionary<string, object>) kvp.Value).ToExpando();
                    expandoDic.Add(kvp.Key, expandoValue);
                }
                else if (kvp.Value is ICollection)
                {
                    // iterate through the collection and convert any strin-object dictionaries
                    // along the way into expando objects
                    var itemList = new List<object>();
                    foreach (var item in (ICollection) kvp.Value)
                    {
                        if (item is IDictionary<string, object>)
                        {
                            var expandoItem = ((IDictionary<string, object>) item).ToExpando();
                            itemList.Add(expandoItem);
                        }
                        else
                        {
                            itemList.Add(item);
                        }
                    }

                    expandoDic.Add(kvp.Key, itemList);
                }
                else
                {
                    expandoDic.Add(kvp);
                }
            }

            return expando;
        }
    }
}
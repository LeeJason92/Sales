namespace PersonaService.Models
{
    public class ModelHelper
    {
        public static void CopyProperty(MPersona source, ref MPersona target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }


        public static void SetEmptyValue(ref MPersona obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetValue(obj) == null)
                {
                    if (prop.PropertyType == typeof(string))
                        prop.SetValue(obj, "");
                    else if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    {
                        // Nullable
                        if (prop.PropertyType == typeof(Nullable<DateTime>))
                            prop.SetValue(obj, new DateTime(1900, 1, 1));
                        else if (prop.PropertyType == typeof(Nullable<int>))
                            prop.SetValue(obj, 0);
                        else if (prop.PropertyType == typeof(Nullable<decimal>))
                            prop.SetValue(obj, 0);
                        else if (prop.PropertyType == typeof(Nullable<double>))
                            prop.SetValue(obj, 0.0);
                        else if (prop.PropertyType == typeof(Nullable<bool>))
                            prop.SetValue(obj, false);
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}

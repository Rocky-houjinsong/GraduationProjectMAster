﻿using System.Reflection;
using System.Text;

namespace ToRead.Modules.Models;

/// <summary>
/// ListView可以在NamedColor类的帮助下显示.NET MAUI中可用的每种命名颜色的列表：
/// </summary>
public class NamedColor
{
    public string Name { get; private set; }
    public string FriendlyName { get; private set; }
    public Color Color { get; private set; }

    // Expose the Color fields as properties
    public float Red => Color.Red;
    public float Green => Color.Green;
    public float Blue => Color.Blue;

    public static IEnumerable<NamedColor> All { get; private set; }

    static NamedColor()
    {
        List<NamedColor> all = new List<NamedColor>();
        StringBuilder stringBuilder = new StringBuilder();

        // Loop through the public static fields of the Color structure.
        foreach (FieldInfo fieldInfo in typeof(Colors).GetRuntimeFields())
        {
            if (fieldInfo.IsPublic &&
                fieldInfo.IsStatic &&
                fieldInfo.FieldType == typeof(Color))
            {
                // Convert the name to a friendly name.
                string name = fieldInfo.Name;
                stringBuilder.Clear();
                int index = 0;

                foreach (char ch in name)
                {
                    if (index != 0 && Char.IsUpper(ch))
                    {
                        stringBuilder.Append(' ');
                    }

                    stringBuilder.Append(ch);
                    index++;
                }

                // Instantiate a NamedColor object.
                NamedColor namedColor = new NamedColor
                {
                    Name = name,
                    FriendlyName = stringBuilder.ToString(),
                    Color = (Color)fieldInfo.GetValue(null)
                };

                // Add it to the collection.
                all.Add(namedColor);
            }
        }

        all.TrimExcess();
        All = all;
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Skybrud.Colors;

namespace Skybrud.Umbraco.PrimaryColors.Models {

    public class PrimaryColorsModel {

        #region Properties

        /// <summary>
        /// Gets a reference to the primary/selected color.
        /// </summary>
        public RgbColor Color { get; private set; }

        /// <summary>
        /// Gets the selected color as a HEX value.
        /// </summary>
        public string ColorAsHex {
            get { return Color.ToHex(); }
        }
        
        /// <summary>
        /// Gets an array of the primary colors extracted from the image.
        /// </summary>
        public RgbColor[] PrimaryColors { get; private set; }

        #endregion

        #region Constructors

        public PrimaryColorsModel(RgbColor color, RgbColor[] primaryColors) {
            Color = color ?? new RgbColor(120, 120, 120);
            PrimaryColors = primaryColors ?? new RgbColor[0];
        }

        public PrimaryColorsModel() : this(null, null) { }

        #endregion

        #region Statics

        public static PrimaryColorsModel Deserialize(string str) {

            // Check whether we have a valid string
            if (String.IsNullOrWhiteSpace(str)) return new PrimaryColorsModel();

            // Split the value into multiple lines
            string[] lines = str.Replace("\r\n", "\n").Split('\n');

            // Parse the selected color
            RgbColor primary;
            if (!TryParseHex(lines[0], out primary)) primary = new RgbColor(102, 102, 102);

            RgbColor[] primaryColors = (lines.Length > 1 ? ParseMultipleHex(lines[1]) : new RgbColor[0]);

            return new PrimaryColorsModel(primary, primaryColors);

        }

        public static RgbColor[] ParseMultipleHex(string str) {
            List<RgbColor> colors = new List<RgbColor>();
            foreach (string piece in str.Split(' ')) {
                RgbColor color;
                if (TryParseHex(piece, out color)) colors.Add(color);
            }
            return colors.ToArray();
        }

        public static bool TryParseHex(string str, out RgbColor color) {
            
            color = null;
            if (String.IsNullOrWhiteSpace(str)) return false;
            
            Match m1 = Regex.Match(str, "^#([0-9a-f]{1})([0-9a-f]{1})([0-9a-f]{1})$");
            Match m2 = Regex.Match(str, "^#([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})$");

            if (m1.Success) {
                int r = Int32.Parse(m1.Groups[1].Value + m1.Groups[1].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                int g = Int32.Parse(m1.Groups[2].Value + m1.Groups[2].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                int b = Int32.Parse(m1.Groups[3].Value + m1.Groups[3].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                color = new RgbColor(r, g, b);
                return true;
            }

            if (m2.Success) {
                int r = Int32.Parse(m2.Groups[1].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                int g = Int32.Parse(m2.Groups[2].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                int b = Int32.Parse(m2.Groups[3].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
                color = new RgbColor(r, g, b);
                return true;
            }

            return false;

        }

        #endregion
    
    }

}
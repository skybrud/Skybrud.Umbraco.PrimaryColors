﻿using System;
using Skybrud.Umbraco.PrimaryColors.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.PrimaryColors {

    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    public class PrimaryColorsPropertyValueConverter : IPropertyValueConverterMeta {

        public bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias == "Skybrud.PrimaryColors";
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview) {
            return source;
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview) {

            // Get the value as a string
            string str = source as string;

            // Deserialize the string
            return PrimaryColorsModel.Deserialize(str);

        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue) {
            PropertyCacheLevel propertyCacheLevel;
            switch (cacheValue) {
                case PropertyCacheValue.Source:
                    propertyCacheLevel = PropertyCacheLevel.Content;
                    break;
                case PropertyCacheValue.Object:
                    propertyCacheLevel = PropertyCacheLevel.ContentCache;
                    break;
                case PropertyCacheValue.XPath:
                    propertyCacheLevel = PropertyCacheLevel.Content;
                    break;
                default:
                    propertyCacheLevel = PropertyCacheLevel.None;
                    break;
            }
            return propertyCacheLevel;
        }
        
        public virtual Type GetPropertyValueType(PublishedPropertyType propertyType) {
            return typeof(PrimaryColorsModel);
        }

    }

}
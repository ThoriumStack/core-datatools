using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBucks.Core.DataTools
{
    public static class TypeMapper
    {
        public static TDestination MapSingle<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();

            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapSingle<TSource, TDestination>(TSource source, TDestination destination)
        {
            // todo: cache maps?
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map(source, destination);
        }

        public static TDestination MapSingleSkipNullOrEmpty<TSource, TDestination>(TSource source, TDestination destination)
        {
            // todo: cache maps?
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();

                cfg.ShouldMapProperty = (prop) => {
                    var sourceProp = source.GetType().GetProperty(prop.Name);
                    if (sourceProp == null)
                    {
                        return false;
                    }
                    var propertyValue = sourceProp.GetValue(source);
                    var isNullOrEmpty = (propertyValue == null || (sourceProp.PropertyType == typeof(string) && string.IsNullOrEmpty((string)propertyValue)));
                    return !isNullOrEmpty;
                };
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map(source, destination);
        }

        public static List<TDestination> MapMany<TSource, TDestination>(List<TSource> sourceList)
        {
            var newList = new List<TDestination>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            foreach (var sourceItem in sourceList)
            {
                var mappedItem = TypeMapper.MapSingle<TSource, TDestination>(sourceItem);
                newList.Add(mappedItem);
            }

            return newList;
        }

        public static TDestination GetNewPopulatedObject<TSource, TDestination>(TSource sourceObject)
           where TDestination : new()
        {
            var newObject = new TDestination();
            MapSingle(sourceObject, newObject);
            return newObject;
        }
    }
}

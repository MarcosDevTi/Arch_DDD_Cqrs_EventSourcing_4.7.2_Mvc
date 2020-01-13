using Arch.Cqrs.Client.Query.Generics;
using Arch.Infra.Data;
using Arch.Infra.Shared.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Arch.Cqrs.Handlers.Generic
{
    public class CsvQueryHandler :
        IQueryHandler<GetObjectsCsv, IEnumerable<object>>
    {
        private readonly ArchCoreContext _architectureContext;
        public CsvQueryHandler(ArchCoreContext architectureContext)
        {
            _architectureContext = architectureContext;
        }
        public IEnumerable<object> Handle(GetObjectsCsv query)
        {
            var props = query.Properties.Split(',');
            var customer = new Domain.Models.Customer();
            var type = customer.GetType().Assembly.GetType(query.ModelAssemblyFullName);

            var result = typeof(Repository<>).MakeGenericType(type);
            dynamic inst = Activator.CreateInstance(result);

            var lista = inst.GetAll(query.Order);

            var listResult = new List<object>();
            foreach (var item in lista)
            {
                var rr = Convert(item, props);
                listResult.Add(rr);
            }
            //var res =((IEnumerable<dynamic>)lista).Select(_ => Convert(_));
            return listResult;
        }

        private ExpandoObject Convert(object obj, string[] properties)
        {
            var expando = new ExpandoObject();
            foreach (var propName in properties)
            {
                var res = obj.GetType().GetProperty(propName).GetValue(obj, null);
                AddProperty(expando, propName, res);
            }

            return expando;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            expandoDict.Add(propertyName, propertyValue);
        }
    }
}

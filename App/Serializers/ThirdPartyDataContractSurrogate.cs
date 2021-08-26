using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using App.Models.ThirdPartySerialization;

namespace App.Serializers
{
    public class ThirdPartyDataContractSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            switch (obj)
            {
                case Address address:
                    return GetAddressToSerialize(address);
                case Employee employee:
                    return GetEmployeeToSerialize(employee);
                default:
                    return obj;
            }
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }

        private static object GetAddressToSerialize(Address address)
        {
            return new Address
            {
                City = address.City,
                Country = address.Country
            };
        }

        private static object GetEmployeeToSerialize(Employee employee)
        {
            return new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                Address = employee.Address
            };
        }
    }
}
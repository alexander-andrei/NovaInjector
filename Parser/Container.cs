using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NovaInjector.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaInjector.Parser
{
    public class Container
    {
        private string _serviceNamespace;
        private string _jsonString;

        public Container(string ServicesFile, string ServiceNamespace)
        {
            _serviceNamespace = ServiceNamespace;
            _jsonString = String.Join("", File.ReadAllLines(ServicesFile));
        }

        public object ParseJsonFile()
        {
            var obj = GetContainerSerializedObjectData(_jsonString, _serviceNamespace);

            if (obj == null)
            {
                throw new Exception("Class " + _serviceNamespace + " does not exist in container.");
            }

            return CreateObjectFromContainer(obj);
        }

        private ContainerClass GetContainerSerializedObjectData(string JsonString, string ServiceNamespace)
        {
            JArray a = JArray.Parse(JsonString);

            foreach (JObject o in a.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    if (p.Name == ServiceNamespace)
                    {
                        var SerializedObject = JsonConvert.DeserializeObject<ContainerClass>(p.Value.ToString());
                        SerializedObject.Name = ServiceNamespace;

                        return SerializedObject;
                    }
                }
            }

            return null;
        }

        private object CreateObjectFromContainer(ContainerClass JsonSerializedObject)
        {
            var NullArguments = JsonSerializedObject.Constructor;

            if (NullArguments == null)
            {
                return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name));
            }

            int  Arguments = JsonSerializedObject.Constructor.Count();

            switch (Arguments)
            {
                case 0:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name));
                case 1:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0])
                        );
                case 2:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1])
                        );
                case 3:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name), 
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[2])
                        );
                case 4:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[2]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[3])
                        );
                case 5:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[2]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[3]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[4])
                        );
                case 6:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[2]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[3]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[4]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[5])
                        );
                case 7:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[0]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[1]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[2]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[3]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[4]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[5]),
                        CheckIfArgumentIsClass(JsonSerializedObject.Constructor[6])
                        );
                default:
                    throw new Exception("Too many constructor arguments for " + JsonSerializedObject.Name + " class.");
            }
        }

        private object CheckIfArgumentIsClass(string Argument)
        {
            if (Type.GetType(Argument) != null)
            {
                _serviceNamespace = Argument;

                var ObjectInContainer = GetContainerSerializedObjectData(_jsonString, Argument);

                if (ObjectInContainer != null)
                {
                    return CreateObjectFromContainer(ObjectInContainer);
                }

                return Activator.CreateInstance(Type.GetType(Argument));
            }

            return Argument;
        }
    }
}

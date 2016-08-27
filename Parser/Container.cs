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
        private string _servicesFile;
        private string _serviceNamespace;

        public Container(string ServicesFile, string ServiceNamespace)
        {
            _servicesFile = ServicesFile;
            _serviceNamespace = ServiceNamespace;
        }

        public object ParseJsonFile()
        {
            string[] JsonArray = File.ReadAllLines(_servicesFile);
            string JsonString = String.Join("", JsonArray);

            var obj = GetContainerSerializedObjectData(JsonString, _serviceNamespace);

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

        public object CreateObjectFromContainer(ContainerClass JsonSerializedObject)
        {
            int Arguments = JsonSerializedObject.Constructor.Count;

            switch (Arguments)
            {
                case 0:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name));
                case 1:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        JsonSerializedObject.Constructor[0]
                        );
                case 2:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name), 
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1]
                        );
                case 3:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name), 
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1],
                        JsonSerializedObject.Constructor[2]
                        );
                case 4:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1],
                        JsonSerializedObject.Constructor[2],
                        JsonSerializedObject.Constructor[3]
                        );
                case 5:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1],
                        JsonSerializedObject.Constructor[2],
                        JsonSerializedObject.Constructor[3],
                        JsonSerializedObject.Constructor[4]
                        );
                case 6:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1],
                        JsonSerializedObject.Constructor[2],
                        JsonSerializedObject.Constructor[3],
                        JsonSerializedObject.Constructor[4],
                        JsonSerializedObject.Constructor[5]
                        );
                case 7:
                    return Activator.CreateInstance(Type.GetType(JsonSerializedObject.Name),
                        JsonSerializedObject.Constructor[0],
                        JsonSerializedObject.Constructor[1],
                        JsonSerializedObject.Constructor[2],
                        JsonSerializedObject.Constructor[3],
                        JsonSerializedObject.Constructor[4],
                        JsonSerializedObject.Constructor[5],
                        JsonSerializedObject.Constructor[6]
                        );
                default:
                    throw new Exception("Too many constructor arguments for " + JsonSerializedObject.Name + " class.");
            }
        }
    }
}

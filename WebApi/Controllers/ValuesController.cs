using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using CalculatorActor.Interfaces;
using Common;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private const string CalculatorActorService = "CalculatorActorService";

        // GET api/values/5/6 
        public async Task<string> Get(int arg1, int arg2)
        {
            ServiceUriBuilder actorServiceUriBuilder = new ServiceUriBuilder(CalculatorActorService);
            
            // This only creates a proxy object, it does not activate an actor or invoke any methods yet.
            ICalculatorActor calculatorActor = ActorProxy.Create<ICalculatorActor>(new ActorId(0), actorServiceUriBuilder.ToUri());

            // This will invoke a method on the actor. If an actor with the given ID does not exist, it will be activated by this method call.
            var result = await calculatorActor.GetSumAsync(arg1, arg2);

            return result.ToString();
        }
    }
}

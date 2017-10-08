using System.Web.Http;
using AopUnityFizzBuzzSpike.Questions;
using UnityInterception;

namespace AopUnityFizzBuzzSpike.Controllers
{
    public class FizzBuzzUnityController : ApiController, IFizzBuzzController
    {
        private readonly IQuestion _question;

        public FizzBuzzUnityController() : this(new UnityProxy<IQuestion>()) { }

        private FizzBuzzUnityController(UnityProxy<IQuestion> unityProxy) : this(unityProxy.Intercept(
            new FizzBuzz(unityProxy.Intercept(
                new Buzz(unityProxy.Intercept(
                    new Fizz(unityProxy.Intercept(
                        new Default())
                    ))))))) { }

        public FizzBuzzUnityController(IQuestion question)
        {
            _question = question;
        }

        public string Get(int id)
        {
            return _question.String(id);
        }
    }

    public interface IFizzBuzzController
    {
        string Get(int id);
    }
}

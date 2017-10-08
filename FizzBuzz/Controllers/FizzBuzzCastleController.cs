using System.Web.Http;
using AopUnityFizzBuzzSpike.Questions;
using CastleInterception;
using Logging;
using Logging.Destinations;

namespace AopUnityFizzBuzzSpike.Controllers
{
    public class FizzBuzzCastleController : ApiController, IFizzBuzzController
    {
        private readonly IQuestion _question;

        public FizzBuzzCastleController() : this(new ApplicationInsightsLog()) { }

        public FizzBuzzCastleController(ILog log) : this(new CastleProxy<IQuestion>(log)) { }

        public FizzBuzzCastleController(CastleProxy<IQuestion> castleProxy) :
            this(castleProxy.Interceptor(new FizzBuzz(castleProxy.Interceptor(new Buzz(castleProxy.Interceptor(new Fizz(castleProxy.Interceptor(new Default())))))))) { }

        public FizzBuzzCastleController(IQuestion question)
        {
            _question = question;
        }

        public string Get(int id)
        {
            return _question.String(id);
        }
    }
}
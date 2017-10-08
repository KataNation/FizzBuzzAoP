namespace AopUnityFizzBuzzSpike.Questions
{
    public class Buzz : IQuestion
    {
        private readonly IQuestion _nextQuestion;

        public Buzz() : this(new Fizz()) { }

        public Buzz(IQuestion nextQuestion)
        {
            _nextQuestion = nextQuestion;
        }

        public string String(int input)
        {
            if (input % 5 == 0) { return "buzz"; }

            return _nextQuestion.String(input);
        }
    }
}
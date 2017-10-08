namespace AopUnityFizzBuzzSpike.Questions
{
    public class FizzBuzz : IQuestion
    {
        private readonly IQuestion _nextQuestion;

        public FizzBuzz() : this(new Buzz()) { }

        public FizzBuzz(IQuestion nextQuestion)
        {
            _nextQuestion = nextQuestion;
        }

        public string String(int input)
        {
            if (input % 3 != 0 || input % 5 != 0) { return _nextQuestion.String(input); }

            return "fizzbuzz";
        }
    }
}
namespace AopUnityFizzBuzzSpike.Questions
{
    public class Fizz : IQuestion
    {
        private readonly IQuestion _nextQuestion;

        public Fizz() : this(new Default()) { }

        public Fizz(IQuestion nextQuestion)
        {
            _nextQuestion = nextQuestion;
        }

        public string String(int input)
        {
            if (input % 3 == 0) { return "fizz"; }

            return _nextQuestion.String(input);
        }
    }
}
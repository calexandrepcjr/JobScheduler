using JobLib;
using NUnit.Framework;

namespace Test
{
    class SchedulerQueueScoreSpec
    {
        [Test]
        public void RespondsToGetScoreAsMinus1WhenThereIsNoScore()
        {
            var queueScore = new SchedulerQueueScore();

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.AreEqual(-1, queueScore.GetScore(0));
        }

        [Test]
        public void RespondsToGetScoreWhenThereIsScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore();
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.AreEqual(5, queueScore.GetScore(queuePosition));
        }

        [Test]
        public void RespondsToRefreshScoreAddingWhenThereIsAlreadyScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore();
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.AreEqual(10, queueScore.GetScore(queuePosition));
        }

        [Test]
        public void RespondsToHasReachedScoreAsTrueWhenMaxScoreIsReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore();
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasReachedScore(queuePosition, 10));
        }

        [Test]
        public void RespondsToHasReachedScoreAsTrueWhenMaxScoreIsSurpassed()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore();
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasReachedScore(queuePosition, 5));
        }

        [Test]
        public void RespondsToHasReachedScoreAsFalseWhenMaxScoreIsNotReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore();
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasReachedScore(queuePosition, 20));
        }
    }
}

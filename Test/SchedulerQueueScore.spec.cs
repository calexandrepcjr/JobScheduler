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
        public void RespondsToUpdateMaxScoreChangingTheMaxScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(10);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));

            queueScore.UpdateMaxScore(30);
            Assert.False(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueReachedScoreAsTrueWhenMaxScoreIsReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(10);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueReachedScoreAsTrueWhenMaxScoreIsReachedPuttingAdditionalScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(30);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition, 20));
        }

        [Test]
        public void RespondsToHasQueueReachedScoreAsFalseWhenMaxScoreIsNotReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(30);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueReachedScoreAsTrueWhenMaxScoreIsSurpassed()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(5);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueEqualsScoreAsTrueWhenMaxScoreIsReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(10);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueEqualsScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueEqualsScoreAsTrueWhenMaxScoreIsReachedPuttingAdditionalScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(30);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueEqualsScore(queuePosition, 20));
        }

        [Test]
        public void RespondsToHasQueueEqualsScoreAsFalseWhenMaxScoreIsNotEqual()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(30);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasQueueEqualsScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueEqualsScoreAsFalseWhenMaxScoreIsSurpassed()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(5);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasQueueEqualsScore(queuePosition));
        }

        [Test]
        public void RespondsToHasQueueEqualsScoreAsFalseWhenMaxScoreIsNotReached()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(20);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasQueueEqualsScore(queuePosition));
        }

        [Test]
        public void RespondsToResetScoreSettingTheScoreAs0WhenDefault()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(10);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition, 10));

            queueScore.UpdateMaxScore(0);
            queueScore.ResetScore(queuePosition);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToResetScoreSettingTheScore()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(10);
            queueScore.RefreshScore(queuePosition, 5);
            queueScore.RefreshScore(queuePosition, 5);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));

            queueScore.UpdateMaxScore(20);
            queueScore.ResetScore(queuePosition, 20);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToResetScoreSettingTheScoreEvenWhenThereIsNoScoreAtPosition()
        {
            var queuePosition = 0;
            var queueScore = new SchedulerQueueScore(20);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            queueScore.ResetScore(queuePosition, 20);
            Assert.True(queueScore.HasQueueReachedScore(queuePosition));
        }

        [Test]
        public void RespondsToHasReachedScore()
        {
            var queueScore = new SchedulerQueueScore(20);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.HasReachedScore(20));
        }

        [Test]
        public void RespondsToHasReachedScoreAsFalseWhenMaxScoreIsNotReached()
        {
            var queueScore = new SchedulerQueueScore(20);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.HasReachedScore(10));
        }

        [Test]
        public void RespondsToEqualsScore()
        {
            var queueScore = new SchedulerQueueScore(20);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.True(queueScore.EqualsScore(20));
        }

        [Test]
        public void RespondsToEqualsScoreAsFalseWhenMaxScoreIsNotEqual()
        {
            var queueScore = new SchedulerQueueScore(20);

            Assert.IsInstanceOf(typeof(SchedulerQueueScore), queueScore);
            Assert.False(queueScore.EqualsScore(21));
        }
    }
}

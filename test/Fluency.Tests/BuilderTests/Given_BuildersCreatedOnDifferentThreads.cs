using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fluency.Tests.BuilderTests
{
    public class Given_BuildersCreatedOnDifferentThreads
    {
        public class ClassWithId
        {
            public int Id { get; set; }
        }


        public class DifferentClassWithId
        {
            public int Id { get; set; }
        }


        public class BuilderWithId : FluentBuilder<ClassWithId>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.Id, GenerateNewId());
            }
        }


        public class DifferentBuilderWithId : FluentBuilder<DifferentClassWithId>
        {
            protected override void SetupDefaultValues()
            {
                SetProperty(x => x.Id, GenerateNewId());
            }
        }

        public class and_the_constructor_calls_into_static_variables : Given_BuildersCreatedOnDifferentThreads
        {
            [Fact]
            public void should_be_thread_safe()
            {
                int numberOfTasks = 8;
                Barrier barrier = new Barrier(numberOfTasks);
                List<Task> builderTasks = new List<Task>();
                for (int j = 0; j < numberOfTasks; j++)
                {
                    builderTasks.Add(new Task(() =>
                    {
                        barrier.SignalAndWait();
                        var builder1 = new BuilderWithId();
                        var builder2 = new DifferentBuilderWithId();
                    }));
                }

                builderTasks.ForEach(x => x.Start());
                builderTasks.ForEach(x => x.Wait());
            }
        }
    }
}
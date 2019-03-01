using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CarPark.Api.ApplicationCore.Services;

namespace CarPark.Test
{
    public class CoreUnitTest
    {
        [Fact]

        public async void BuildNamesSucceeded()
        {
            QueueService q = new QueueService();

            q.Buildnames(10);
            

        }

    }
}

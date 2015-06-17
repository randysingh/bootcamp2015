using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoApp.Common.Services;
using DemoApp.Common.Interfaces;
using Moq;
using System.Collections.Generic;
using DemoApp.Common.Models;

namespace DemoApp.Tests
{
    [TestClass]
    public class MyJobsServiceTests
    {
        private MyJobsService _subject;
        private Mock<ILocalDataService> _mockData;

        [TestInitialize]
        public void Init()
        {
            _mockData = new Mock<ILocalDataService>();

            var dict = new Dictionary<string, string>();

            _mockData.Setup(s => s.GetValue(It.IsAny<string>()))
                .Returns((string p) => dict.ContainsKey(p) ? dict[p] : null);

            _mockData.Setup(s => s.SetValue(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string k, string v) => dict[k] = v);

            _subject = new MyJobsService(_mockData.Object);
        }

        [TestMethod]
        public void SaveJob_ThenGetJob_JobIsIncluded()
        {
            var job = new Job() { JobId = "foo", Title = "Hello", MaxSalary = 1d };

            _subject.AddToMyJobs(job);

            var jobs = _subject.GetMyJobs();

            Assert.AreEqual(1, jobs.Length);

            Assert.AreEqual("foo", jobs[0].JobId);
            Assert.AreEqual(1d, jobs[0].MaxSalary);
        }

        [TestMethod]
        public void Save2Job__SameId_ThenGetJob_1JobIsIncluded()
        {
            var job = new Job() { JobId = "foo", Title = "Hello", MaxSalary = 1d };

            _subject.AddToMyJobs(job);

            job.MaxSalary = 2d;

            _subject.AddToMyJobs(job);

            var jobs = _subject.GetMyJobs();

            Assert.AreEqual(1, jobs.Length);

            Assert.AreEqual("foo", jobs[0].JobId);
            Assert.AreEqual(1d, jobs[0].MaxSalary);
        }

        [TestMethod]
        public void Save2Job__DifferentId_ThenGetJob_2JobIsIncluded()
        {
            var job = new Job() { JobId = "foo", Title = "Hello", MaxSalary = 1d };

            _subject.AddToMyJobs(job);

            job.MaxSalary = 2d;
            job.JobId = "bar";

            _subject.AddToMyJobs(job);

            var jobs = _subject.GetMyJobs();

            Assert.AreEqual(2, jobs.Length);

            Assert.AreEqual("foo", jobs[0].JobId);
            Assert.AreEqual(1d, jobs[0].MaxSalary);
        }
    }
}

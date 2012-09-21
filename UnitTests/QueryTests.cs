using System;
using NUnit.Framework;
using System.Configuration;
using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Configuration.Fluent;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Query;


namespace UnitTests
{
	[TestFixture]
	public class QueryTests
	{
		private Cube<int> cube;
		
		[Test]
		public void TestMethod()
		{
			//cube.Query().Init(
		}
		
		[TestFixtureSetUp]
		public void Init()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource((sourcebuild) => sourcebuild.SetSource("sales"))
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("sales")
				               		.SetSourceType(DataSourceType.CSV)
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("TestData//table.csv")
				               		              		.SetHasHeader();
				               		              })
				               		.AddField("category", 0, typeof(int))
				               		.AddField("sex", 1, typeof(int))
				               		.AddField("place", 2, typeof(int))
				               		.AddField("expenses", 3, typeof(double))
				               		.AddField("items", 4, typeof(int));
				               })
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("categories")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("id", 0, typeof(int))
				               		.AddField("description", 1, typeof(string))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("TestData//dimension1.csv")
				               		              		.SetHasHeader();
				               		              });
				               })
				.MetaData(mbuild => {
				          	mbuild.AddDimension("category", (dimbuild)=> {
				          	                    	dimbuild.Source("categories")
				          	                    		.ValueField("id")
				          	                    		.DescField("description");
				          	                    });
				          });
			
			cube = builder.Create<int>();
			cube.Initialize();
		}
		
		[TestFixtureTearDown]
		public void Dispose()
		{
			cube.Dispose();
		}
	}
}

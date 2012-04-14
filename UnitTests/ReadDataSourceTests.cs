using System;
using NUnit.Framework;
using System.Configuration;
using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Configuration.Fluent;
using NSimpleOLAP.Schema;

namespace UnitTests
{
	[TestFixture]
	public class ReadDataSourceTests
	{
		[Test]
		public void DimensionMembersTest()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource((sourcebuild) => {
				           	sourcebuild.SetSource("sales")
				           		.AddMapping("category", "category")
				           		.AddMapping("sex","sex")
				           		.AddMapping("place", "place");
				           })
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
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("sexes")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("id", 0, typeof(int))
				               		.AddField("description", 1, typeof(string))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("TestData//dimension2.csv")
				               		              		.SetHasHeader();
				               		              });
				               })
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("places")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("id", 0, typeof(int))
				               		.AddField("description", 1, typeof(string))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("TestData//dimension3.csv")
				               		              		.SetHasHeader();
				               		              });
				               })
				.MetaData(mbuild => {
				          	mbuild.AddDimension("category", (dimbuild)=> {
				          	                    	dimbuild.Source("categories")
				          	                    		.ValueField("id")
				          	                    		.DescField("description");
				          	                    })
				          		.AddDimension("sex", (dimbuild)=> {
				          	                    	dimbuild.Source("sexes")
				          	                    		.ValueField("id")
				          	                    		.DescField("description");
				          	                    })
				          		.AddDimension("place", (dimbuild)=> {
				          	                    	dimbuild.Source("places")
				          	                    		.ValueField("id")
				          	                    		.DescField("description");
				          	                    });
				          });
			
			Cube<int> cube = builder.Create<int>();
			
			cube.Initialize();
			cube.Process();
			
			Assert.AreEqual("male",cube.Schema.Dimensions["sex"].Members["male"].Name);
			Assert.AreEqual("female",cube.Schema.Dimensions["sex"].Members["female"].Name);
			Assert.AreEqual("London",cube.Schema.Dimensions["place"].Members["London"].Name);
		}
	}
}

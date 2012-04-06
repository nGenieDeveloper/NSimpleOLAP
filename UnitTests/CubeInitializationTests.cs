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
	public class CubeInitializationTests
	{
		[Test]
		public void DefaultSettingsInit_Test()
		{
			Cube<int> cube = new Cube<int>();
			
			cube.Initialize();
			
			Assert.AreEqual(StorageType.Molap,cube.Storage.StorageType);
		}
		
		[Test]
		public void MolapAddDimensionInit_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("xpto")
				               		.SetSourceType(DataSourceType.CSV)
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("xpto.csv");
				               		              });
				               })
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("xtable")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("xkey", 0, typeof(int))
				               		.AddField("xdesc", 1, typeof(string))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("xtable.csv");
				               		              });
				               })
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", (dimbuild)=> {
				          	                    	dimbuild.Source("xtable")
				          	                    		.ValueField("xkey")
				          	                    		.DescField("xdesc");
				          	                    });
				          });
			
			Cube<int> cube = builder.Create<int>();
			
			cube.Initialize();
			
			Assert.AreEqual("x",cube.Schema.Dimensions["x"].Name);
			Assert.AreEqual("xtable",cube.Schema.Dimensions["x"].DataSource.Name);
			Assert.AreEqual(ItemType.Dimension,cube.Schema.Dimensions["x"].ItemType);
			Assert.Greater(cube.Schema.Dimensions["x"].ID, 0);
		}
		
		[Test]
		public void MolapAddMeasureInit_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("xpto")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("x", 0, typeof(int))
				               		.AddField("varx1", 2, typeof(int))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("xpto.csv");
				               		              });
				               })
				.AddDataSource(dsbuild => {
				               	dsbuild.SetName("xtable")
				               		.SetSourceType(DataSourceType.CSV)
				               		.AddField("xkey", 0, typeof(int))
				               		.AddField("xdesc", 1, typeof(string))
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("xtable.csv");
				               		              });
				               })
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", (dimbuild)=> {
				          	                    	dimbuild.Source("xtable")
				          	                    		.ValueField("xkey")
				          	                    		.DescField("xdesc");
				          	                    })
				          		.AddMeasure("var1", mesbuild => {
				          	                  	mesbuild.ValueField("varx1")
				          	                  		.SetType(typeof(int));
				          	                  });
				          });
			
			Cube<int> cube = builder.Create<int>();
			
			cube.Initialize();
			
			Assert.AreEqual("var1", cube.Schema.Measures["var1"].Name);
			Assert.AreEqual(ItemType.Measure, cube.Schema.Measures["var1"].ItemType);
			Assert.AreEqual(typeof(int), cube.Schema.Measures["var1"].DataType);
		}
	}
}

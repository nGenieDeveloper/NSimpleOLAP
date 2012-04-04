using System;
using NUnit.Framework;
using System.Configuration;
using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Configuration.Fluent;


namespace UnitTests
{
	[TestFixture]
	public class ConfigTests
	{
		[Test]
		public void SetNameConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello");
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("hello", cube.Name);
		}
		
		[Test]
		public void SetSourceConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto");
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("xpto", cube.Source);
		}
		
		[Test]
		public void DefaultConfigName_Test()
		{
			Cube<int> cube = new Cube<int>();
			
			Assert.AreEqual("New_Cube", cube.Name);
		}
		
		[Test]
		public void DefaultConfigStorage_Test()
		{
			Cube<int> cube = new Cube<int>();
			
			Assert.AreEqual(StorageType.Molap, cube.Config.Storage.StoreType);
		}
		
		[Test]
		public void AddDimensionConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", (dimbuild)=> {
				          	                    	dimbuild.Source("xtable")
				          	                    		.ValueField("xkey")
				          	                    		.DescField("xdesc");
				          	                    });
				          });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("x", cube.Config.MetaData.Dimensions[0].Name);
			Assert.AreEqual("x", cube.Config.MetaData.Dimensions["x"].Name);
		}
		
		[Test]
		public void DimensionSetupConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", (dimbuild)=> {
				          	                    	dimbuild.Source("xtable")
				          	                    		.ValueField("xkey")
				          	                    		.DescField("xdesc");
				          	                    });
				          });
			
			Cube<int> cube = builder.Create<int>();

			Assert.AreEqual("x", cube.Config.MetaData.Dimensions["x"].Name);
			Assert.AreEqual("xtable", cube.Config.MetaData.Dimensions["x"].Source);
			Assert.AreEqual("xdesc", cube.Config.MetaData.Dimensions["x"].DesFieldName);
			Assert.AreEqual("xkey", cube.Config.MetaData.Dimensions["x"].ValueFieldName);
		}
		
		[Test]
		public void AddMoreThanOneDimensionConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", (dimbuild)=> {
				          	                    	dimbuild.Source("xtable")
				          	                    		.ValueField("xkey")
				          	                    		.DescField("xdesc");
				          	                    })
				          		.AddDimension("y", (dimbuild)=> {
				          	                    	dimbuild.Source("ytable")
				          	                    		.ValueField("ykey")
				          	                    		.DescField("ydesc");
				          		              });
				          });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("x", cube.Config.MetaData.Dimensions[0].Name);
			Assert.AreEqual("x", cube.Config.MetaData.Dimensions["x"].Name);
			Assert.AreEqual("y", cube.Config.MetaData.Dimensions[1].Name);
			Assert.AreEqual("y", cube.Config.MetaData.Dimensions["y"].Name);
		}
		
		[Test]
		public void AddDataSourceConfig_Test()
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
				               });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("xpto", cube.Config.DataSources["xpto"].Name);
		}
		
		[Test]
		public void DataSourceSetupConfig_Test()
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
				               });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("xpto", cube.Config.DataSources["xpto"].Name);
			Assert.AreEqual(DataSourceType.CSV, cube.Config.DataSources["xpto"].SourceType);
			Assert.AreEqual("xpto.csv", cube.Config.DataSources["xpto"].CSVConfig.FilePath);
		}
		
		[Test]
		public void AddMoreThanOneDataSourceConfig_Test()
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
				               		.SetCSVConfig(csvbuild => {
				               		              	csvbuild.SetFilePath("xtable.csv");
				               		              });
				               });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual("xpto", cube.Config.DataSources["xpto"].Name);
			Assert.AreEqual("xtable", cube.Config.DataSources["xtable"].Name);
		}
		
		[Test]
		public void SetStorageConfig_Test()
		{
			CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.Storage(storebuild => {
				         	storebuild.SetStoreType(StorageType.Molap);
				         });
			
			Cube<int> cube = builder.Create<int>();
			
			Assert.AreEqual(StorageType.Molap, cube.Config.Storage.StoreType);
		}
		
		
		/*	
 *example 
* CubeBuilder builder = new CubeBuilder();
			
			builder.SetName("hello")
				.SetSource("xpto")
				.MetaData(mbuild => {
				          	mbuild.AddDimension("x", xdim => {
				          	                    	xdim.Source("xpto")
				          	                    		.DescField("desc")
				          	                    		.ValueField("dim");
				          	                    })
				          		.AddMeasure("q", xmes => {
				          		            	xmes.ValueField("val");
				          		            })
				          		.AddMetric("m", xmet => {});
				          })
				.AddDataSource(xdata => {
				               	xdata.SetName("xpto")
				               		.SetSourceType(DataSourceType.CSV)
				               		.SetCSVConfig(cvsconf => {
				               		              	cvsconf.SetFilePath("c:\any");
				               		              })
				               		.AddField("dim", typeof(int))
				               		.AddField("desc", typeof(string))
				               		.AddField("val", typeof(int));
				               })
				.Storage(xstore => {
				         	xstore.SetStoreType(StorageType.Molap);
				         });
			
			CubeConfig element = builder.Create();
			
			Assert.AreEqual(element.Name, "hello");*/
	}
}

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

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Regions;

namespace Aliyun.Acs.Core.Tests.Units.Profile
{
    public class MultiProfileTest
    {
        [Fact]
        public void Get()
        {
            var regionId = "cn-hangzhou";
            var accessKeyId = "accessKeyId";
            var accessKeysecret = "accessKeysecret";
            var profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeysecret);

            var regionId2 = "cn-hangzhou";
            var accessKeyId2 = "accessKeyId2";
            var accessKeysecret2 = "accessKeysecret2";
            var profile2 = MultiProfile.GetProfile(regionId2, accessKeyId2, accessKeysecret2);


            Assert.Equal(regionId, profile.GetRegionId());
            Assert.Equal(accessKeyId, profile.GetCredential().AccessKeyId);


            Assert.Equal(regionId2, profile2.GetRegionId());
            Assert.Equal(accessKeyId2, profile2.GetCredential().AccessKeyId);

            profile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeysecret);

            Assert.Equal(regionId2, profile2.GetRegionId());
            Assert.Equal(accessKeyId2, profile2.GetCredential().AccessKeyId);
        }


        [Fact]
        public void AddEndpoint()
        {
            var endpointName = "AddEndpoint.someString";
            var regionId = "AddEndpoint.someString";
            var productName = "product_name";
            var productDomain = "product_domain";
            List<Endpoint> endpoints;
            List<ProductDomain> products;

            var profile1 = MultiProfile.GetProfile(regionId,"key1","secret1");
            profile1.AddEndpoint(endpointName, regionId, productName, productDomain);
            endpoints = profile1.GetEndpoints(regionId, productName);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName+" - "+ product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }

            var profile2 = MultiProfile.GetProfile(regionId, "key2", "secret2");
            endpoints = profile2.GetEndpoints(regionId, productName);
            Assert.NotNull(endpoints);
            Assert.Empty(endpoints);

            profile2.AddEndpoint(endpointName, regionId, "product_name2", productDomain);
            endpoints = profile2.GetEndpoints(regionId, productName);
            Assert.NotEmpty(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal("product_name2", product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }
        }


        [Fact]
        public void GetEndpoint()
        {
            var endpointName = "cn-hangzhou";
            var regionId = "cn-hangzhou";
            var productName = "Cloudauth";
            var productDomain = "cloudauth.aliyuncs.com";
            List<Endpoint> endpoints;
            List<ProductDomain> products;

            var profile1 = MultiProfile.GetProfile(regionId, "key1", "secret1");

            //profile.AddEndpoint("cn-hangzhou", "cn-hangzhou", "Cloudauth", "cloudauth.aliyuncs.com");
            profile1.AddEndpoint(endpointName, regionId, productName, productDomain);
            endpoints = profile1.GetEndpoints(productName, regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }

            var profile2 = MultiProfile.GetProfile(regionId, "key2", "secret2");
            endpoints = profile2.GetEndpoints(productName, regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);                
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }
        }

        [Fact]
        public void AddMutileEndPoint()
        {
            var endpointName = "cn-hangzhou";
            var regionId = "cn-hangzhou";
            var productName = "Cloudauth";
            var productDomain = "cloudauth.aliyuncs.com";
            List<Endpoint> endpoints;
            List<ProductDomain> products;

            var profile1 = MultiProfile.GetProfile(regionId, "key1", "secret1");

            profile1.AddEndpoint(endpointName, regionId, productName, productDomain);
            endpoints = profile1.GetEndpoints(productName, regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }

            var profile2 = MultiProfile.GetProfile(regionId, "key1", "secret1");

            profile2.AddEndpoint(endpointName, regionId, productName, productDomain);
            endpoints = profile2.GetEndpoints(productName, regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }
        }

        [Fact]
        public void AddDiffMutileEndPoint()
        {
            var endpointName = "cn-hangzhou";
            var regionId = "cn-hangzhou";
            var productName = "Cloudauth";
            var productDomain = "cloudauth.aliyuncs.com";
            List<Endpoint> endpoints;
            List<ProductDomain> products;

            var profile1 = MultiProfile.GetProfile(regionId, "key1", "secret1");

            profile1.AddEndpoint(endpointName, regionId, productName, productDomain);
            endpoints = profile1.GetEndpoints(productName, regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal(endpointName, endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal(productName, product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }

            var profile2 = MultiProfile.GetProfile(regionId, "key1", "secret1");

            profile2.AddEndpoint("cn-hangzhou2", regionId, "Cloudauth1", productDomain);
            endpoints = profile2.GetEndpoints("Cloudauth1", regionId, null, null);
            Assert.NotNull(endpoints);
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Assert.Equal("cn-hangzhou2", endpoint.Name);
                products = endpoint.ProductDomains;
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " - " + product.DomainName);
                    Assert.Equal("Cloudauth1", product.ProductName);
                    Assert.Equal(productDomain, product.DomainName);
                }
            }
        }

    }
}

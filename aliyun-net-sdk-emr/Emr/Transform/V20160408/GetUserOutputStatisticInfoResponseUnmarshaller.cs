/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System;
using System.Collections.Generic;

using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Emr.Model.V20160408;

namespace Aliyun.Acs.Emr.Transform.V20160408
{
    public class GetUserOutputStatisticInfoResponseUnmarshaller
    {
        public static GetUserOutputStatisticInfoResponse Unmarshall(UnmarshallerContext context)
        {
			GetUserOutputStatisticInfoResponse getUserOutputStatisticInfoResponse = new GetUserOutputStatisticInfoResponse();

			getUserOutputStatisticInfoResponse.HttpResponse = context.HttpResponse;
			getUserOutputStatisticInfoResponse.RequestId = context.StringValue("GetUserOutputStatisticInfo.RequestId");

			List<GetUserOutputStatisticInfoResponse.GetUserOutputStatisticInfo_ClusterStatUserOutput> getUserOutputStatisticInfoResponse_userOutputList = new List<GetUserOutputStatisticInfoResponse.GetUserOutputStatisticInfo_ClusterStatUserOutput>();
			for (int i = 0; i < context.Length("GetUserOutputStatisticInfo.UserOutputList.Length"); i++) {
				GetUserOutputStatisticInfoResponse.GetUserOutputStatisticInfo_ClusterStatUserOutput clusterStatUserOutput = new GetUserOutputStatisticInfoResponse.GetUserOutputStatisticInfo_ClusterStatUserOutput();
				clusterStatUserOutput.User = context.StringValue("GetUserOutputStatisticInfo.UserOutputList["+ i +"].User");
				clusterStatUserOutput.BytesOutput = context.LongValue("GetUserOutputStatisticInfo.UserOutputList["+ i +"].BytesOutput");

				getUserOutputStatisticInfoResponse_userOutputList.Add(clusterStatUserOutput);
			}
			getUserOutputStatisticInfoResponse.UserOutputList = getUserOutputStatisticInfoResponse_userOutputList;
        
			return getUserOutputStatisticInfoResponse;
        }
    }
}

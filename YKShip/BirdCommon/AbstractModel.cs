/*
 * Copyright (c) 2018 THL A29 Limited, a Tencent company. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace YK.Ship.BirdCommon
{
    public abstract class AbstractModel
    {
        /// <summary>
        /// Serialize an obect of AbstractModel into JSON string.
        /// </summary>
        /// <typeparam name="V">A class inherited from AbstrctModel.</typeparam>
        /// <param name="obj">An object of the class.</param>
        /// <returns>JSON formatted string.</returns>
        public static string ToJsonString<V>(V obj) where V : AbstractModel
        {
            var jsonSet = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            jsonSet.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, Formatting.None, jsonSet);
        }

        /// <summary>
        /// Deserialize JSON formatted string into an object of a class inherited from AbstractModel.
        /// </summary>
        /// <typeparam name="V">A class inherited from AbstrctModel.</typeparam>
        /// <param name="json">JSON formatted string.</param>
        /// <returns>An object of the class.</returns>
        public static V FromJsonString<V>(string json)
        {
            return JsonConvert.DeserializeObject<V>(json);
        }
    }
}

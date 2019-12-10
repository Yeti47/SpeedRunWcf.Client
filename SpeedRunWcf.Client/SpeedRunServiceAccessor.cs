using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SpeedRunWcf.Client.SpeedRunWebservice;

namespace SpeedRunWcf.Client {

    internal class SpeedRunServiceAccessor {

        #region Methods

        public SpeedRunFetchResult GetSpeedRunData() {

            SpeedRun[] speedRuns = null;
            ResultType result = ResultType.Success;
            string errMsg = string.Empty;

            SpeedRunWebserviceClient client = new SpeedRunWebserviceClient();

            try {

                // client logic

                SpeedRunServiceRequest request = new SpeedRunServiceRequest();

                SpeedRunServiceResponse response = client.GetSpeedRuns(request);

                if(response.IsError) {

                    result = ResultType.ServerError;
                    errMsg = response.Message;

                }
                else {

                    speedRuns = response.SpeedRuns;

                }

                client.Close();

            }
            catch(Exception ex) {

                result = ResultType.InternalError;
                errMsg = ex.Message;

                client.Abort();

            }

            return new SpeedRunFetchResult(result, speedRuns, errMsg);

        }

        #endregion


    }

}

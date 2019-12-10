using SpeedRunWcf.Client.SpeedRunWebservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedRunWcf.Client {

    public class SpeedRunFetchResult {

        public IEnumerable<SpeedRun> SpeedRuns { get; }

        public int ResultCount => SpeedRuns.Count();

        public ResultType ResultType { get; }

        public string ErrorMessage { get; }

        public SpeedRunFetchResult(ResultType resultType, IEnumerable<SpeedRun> speedRuns, string errorMessage) {

            ResultType = resultType;
            SpeedRuns = speedRuns ?? new SpeedRun[0];
            ErrorMessage = errorMessage ?? string.Empty;

        }

        public override string ToString() {

            string outPut = $"Result: {ResultType} | ResultCount: {ResultCount}";

            if (string.IsNullOrWhiteSpace(ErrorMessage))
                outPut += $"ErrorMessage: {ErrorMessage}";

            return outPut;

        }

    }

}

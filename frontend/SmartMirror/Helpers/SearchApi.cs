using SmartMirror.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirror.Helpers
{
    public static class SearchApi
    {
        public static async Task<string> CallSearchApi(string keyword, int page)
        {
            string baseUrl = $"{ApiConfig.url}/1/goods";
            string urlWithParams = $"{baseUrl}?keyword={keyword}&page={page}&size=9";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(urlWithParams);

                    // 서버 응답이 성공적인지 확인
                    response.EnsureSuccessStatusCode();

                    // 성공적으로 응답을 받았을 때
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException httpEx)
            {
                // HTTP 요청과 관련된 오류 처리
                MessageBox.Show($"검색 결과가 없어요!: {httpEx.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (TaskCanceledException timeoutEx)
            {
                // 타임아웃과 같은 비동기 작업 취소 오류 처리
                MessageBox.Show($"요청이 시간 초과되었습니다: {timeoutEx.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                // 기타 일반적인 예외 처리
                MessageBox.Show($"예기치 않은 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

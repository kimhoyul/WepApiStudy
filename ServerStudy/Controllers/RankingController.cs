using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerStudy.Data;
using ServerStudy.Models;

namespace ServerStudy.Controllers
{

    // REST : 리프리젠테이셔널 스테이트 트랜스퍼
    // HTTP 통신에서 뭔가 일부 기능을 빼가지고 나름대로 좀 간소화 된 특수한 데이터 송수신 규칙을 만든 것
    // HTTP 통신을 활용해 자원 을 CRUD 하는 약속된 규칙
    // 이 규칙을 잘 따르면 RESTful API 부른다

    // [API 응답 반환 규칙]
    // 1. null 반환 시 : HTTP 204 (No Content) - "데이터 없음"을 요청한 클라이언트에 에게 전달
    // 2. string 반환 시 : Text/Plain 형식 - 일반 텍스트 그대로 반환
    // 3. 기타 객체(List/int/bool) 등 : Application/Json 형식 - 자동으로 JSON 형식으로 클라이언트에게 전달 

    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Read (GET) 
        // 자원 조회. Body 비워둠. URL 경로(path)나 쿼리 파라미터로 대상(EndPoint) 지정
        // Get => localhost:9999/api/ranking (전체리스트 조회하겠다)
        // Get => localhost:9999/api/ranking/5 (ID 5인 유저를 조회하겠다) 
        [HttpGet] 
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results = _context.GameResults.OrderByDescending(item => item.Score).ToList();

            return results;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResult(int id)
        {
            GameResult gameResult = _context.GameResults.Where(item => item.Id == id).FirstOrDefault();

            return gameResult;
        }

        // Create (POST)
        // 새로운 자원 생성 or 서버에 내가 데이터를 전송할때. 데이터를 Body 부분에 담아서 전송
        // POST => localhost:9999/api/ranking    |    body : {"id" : "", "userName" : "김영훈", "Score" : "999"}
        [HttpPost]
        public bool AddGameResult([FromBody]GameResult gameResult)
        {
            gameResult.Id = 0;

            _context.GameResults.Add(gameResult);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // Update (PUT / PATCH) => 보통은 PUT(전체수정) 을 많이 사용, 세밀하게 수정 PATHCH(부분수정)도 쓴다.
        // 자원 수정. 변경할 데이터를 Body에 담아서 전송
        // PUT => localhost:9999/api/ranking    |    body : {"userName" : "김영훈", "Score" : "888"}
        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            GameResult findResut = _context.GameResults.Where(item => item.UserId == gameResult.UserId).FirstOrDefault();
            
            findResut.UserName = gameResult.UserName;
            findResut.UserEmail = gameResult.UserEmail;
            findResut.Score = gameResult.Score;
            findResut.Date = gameResult.Date;

            _context.SaveChanges();
            return true;
        }

        // Delete (DELETE)
        // 자원 삭제. 보통 별도의 Body 없이 URL의 ID 만으로 삭제 대상을 지정.
        // DELETE => localhost:9999/api/ranking/5 
        [HttpDelete("{id}")]
        public bool DeleteGameResult(int id)
        {
            GameResult findResult = _context.GameResults.Where(item => item.Id == id).FirstOrDefault();
            if (findResult == null)
                return false;

            _context.GameResults.Remove(findResult);
            _context.SaveChanges();
            return true;
        }
    }
}

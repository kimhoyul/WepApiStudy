namespace ServerStudy.Models
{
    // 데이터클래스 == 모델 // MSSql = 데이터베이스 프로그램
    public class GameResult
    {
        public int Id { get; set; } // 이 결과값의 ID
        public int UserId { get; set; } // 이 결과값을 만든 유저의 ID
        public string UserName { get; set; } // 그유저의 아이디
        public string UserEmail { get; set; } // 그유저의 이메일
        public int Score { get; set; } // 이 결과값의 점수
        public DateTime Date { get; set; } // 이 결과값이 만들어진 시간
    }
}

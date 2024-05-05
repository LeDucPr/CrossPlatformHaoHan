using ApiTruyenLau.Objects.Converters.Users;
using ApiTruyenLau.Services;
using ApiTruyenLau.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using UserCvt = ApiTruyenLau.Objects.Converters.Users;
using ItemCvt = ApiTruyenLau.Objects.Converters.Items;
using ApiTruyenLau.Objects.Converters.Items;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ApiTruyenLau.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IConfiguration _configuration;
        private IBookServices _bookServices;
        private IAccountServices _accountServices;
        private IClientServices _clientServices;
        private ISecurityServices _securityServices;

        public BookController(IBookServices bookServices, IAccountServices accountServices, ILogger<ClientController> logger, IConfiguration configuration, IClientServices clientServices, ISecurityServices securityServices)
        {
            _logger = logger;
            _configuration = configuration;
            _bookServices = bookServices;
            _accountServices = accountServices; // cái này cần để lấy theo yêu cầu người dùng 
            _clientServices = clientServices;
            _securityServices = securityServices;
        }

        #region Phần bìa sách 
        /// <summary>
        /// Lấy phần bìa sách theo id sách
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet("GetCoverById")]
        public async Task<ActionResult<ItemCvt.CoverBookCvt>> GetCoverById(string bookId, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var coverBookCvt = await _bookServices.GetCoverById(bookId);
                return Ok(coverBookCvt);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy các gợi ý nếu người đó đã đọc truyện một vài truyện 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("GetCoversByClientId")]
        public async Task<ActionResult<List<string>>> GetCoversByClientId([FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var suggestBookIds = await _bookServices.GetCoversByClientIds(userId);
                return Ok(suggestBookIds);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy vài quyển sách theo fields -> intro (filter by System)
        /// </summary>
        /// <param name="pbc"></param>
        /// <returns></returns>
        [HttpPut("GetCoversByFields(Equals)")]
        public async Task<ActionResult<List<ItemCvt.CoverBookCvt>>> GetCoversByFieldsEquals([FromBody] ParamForBookCover pbc, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var coverBookPartCvts = await _bookServices.GetCoversByFieldsEquals(pbc.AmountCovers, pbc.SkipIds, pbc.Fields);
                return Ok(coverBookPartCvts);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy vài quyển sách theo fields -> intro (filter by System)
        /// </summary>
        /// <param name="pbc"></param>
        /// <param name="LetterTrueWordFalse">mặc định true là lấy the letter</param>
        /// <param name="amountWords">thông số này không qua tâm khi chỉ lấy ra theo letter (true)</param>
        /// <returns></returns>
        [HttpPut("GetCoversByFields(Contrains)")]
        public async Task<ActionResult<List<ItemCvt.CoverBookCvt>>> GetCoverByFieldsContrains([FromBody] ParamForBookCover pbc, bool LetterTrueWordFalse = true, int amountWords = 1, [FromHeader] string userId = null!, [FromHeader] string token = null!)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var dictFields = LetterTrueWordFalse ? pbc.FieldsToLetter() : pbc.FieldsToWords(amountWords);
                var coverBookPartCvts = await _bookServices.GetCoversByFieldsContrains(pbc.AmountCovers, pbc.SkipIds, dictFields);
                return Ok(coverBookPartCvts);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy vài quyển sách theo số lượng đọc, ưu tiên lấy theo số lượng đọc nhiều nhất
        /// </summary>
        /// <param name="amountCovers"></param>
        /// <param name="skipIds"></param>
        /// <returns></returns>
        [HttpGet("GetCoverByDesReaders")]
        public async Task<ActionResult<List<ItemCvt.CoverBookCvt>>> GetCoverByDesReaders([FromQuery] int amountCovers, [FromQuery] List<string> skipIds, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var coverBookPartCvts = await _bookServices.GetSortedCoversByFields(amountCovers, skipIds, false, nameof(ApiTruyenLau.Objects.Generics.Items.Book.Reader));
                return Ok(coverBookPartCvts);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy ra sách và có chọn lọc theo thể loại truyện
        /// </summary>
        /// <param name="amountCovers"></param>
        /// <param name="title"></param>
        /// <param name="skipIds"></param>
        /// <returns></returns>
        [HttpGet("GetCoverByDesReadersAndGenre")]
        public async Task<ActionResult<List<ItemCvt.CoverBookCvt>>> GetCoverByDesReadersAndTitles([FromQuery] int amountCovers, [FromQuery] string genre, [FromQuery] List<string> skipIds, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var coverBookPartCvts = await _bookServices.GetSortedCoversByFields(amountCovers, skipIds, bookFields: new Dictionary<string, List<string>>() { { "genre", new List<string> { genre } } }, false, nameof(ApiTruyenLau.Objects.Generics.Items.Book.Reader));
                return Ok(coverBookPartCvts);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Lấy vài quyển sách theo ngày xuất bản, ưu tiên ngày gần nhất
        /// </summary>
        /// <param name="amountCovers"></param>
        /// <param name="skipIds"></param>
        /// <returns></returns>
        [HttpGet("GetCoverByDesPublishDate")]
        public async Task<ActionResult<List<ItemCvt.CoverBookCvt>>> GetCoverByDesPublishDate([FromQuery] int amountCovers, [FromQuery] List<string> skipIds, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var coverBookPartCvts = await _bookServices.GetSortedCoversByFields(amountCovers, skipIds, false, nameof(ApiTruyenLau.Objects.Generics.Items.Book.PublishDate));
                return Ok(coverBookPartCvts);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion Phần bìa sách


        #region Phần intro sách
        /// <summary>
        /// Lấy phần intro sách theo id sách
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("GetIntroById")]
        public async Task<ActionResult<ItemCvt.IntroBookPartCvt>> GetIntroById(string bookId, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var introBookPartCvt = await _bookServices.GetIntroById(bookId);
                return Ok(introBookPartCvt);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion Phần intro sách

        #region Phần nội dung sách
        /// <summary>
        /// Lấy nội dung sách theo id sách
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet("GetBookById")]
        public async Task<ActionResult<ItemCvt.BookCvt>> GetBookById(string bookId, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var a = await _bookServices.GetBookById(bookId);
                return Ok(a);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("UpdateBookReader")]
        public async Task<ActionResult> UpdateRatingBook(string bookId, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                await _bookServices.UpdateBookReader(bookId);
                return Ok("Dã thêm một lần đọc");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lấy nội dung sách theo id sách và số lượng trang kế tiếp
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="skipImages"></param>
        /// <param name="takeImages"></param>
        /// <returns></returns>
        [HttpGet("GetNextImagesForContent")]
        public async Task<ActionResult<List<string>>> GetNextImagesForContent(string bookId, int skipImages, int takeImages, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                var images = await _bookServices.GetNextImagesForContent(bookId, skipImages, takeImages);
                return Ok(images);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion Phần nội dung sách

        #region Phần tạo sách
        /// <summary>
        /// Tạo sách mới theo đúng cấu trúc bằng json (tạo với array)
        /// </summary>
        /// <param name="bookCreaterCvts"></param>
        /// <returns></returns>
        [HttpPost("CreateNewBooks")]
        public async Task<ActionResult<string>> CreateBooks([FromBody] List<ItemCvt.BookCreaterCvt> bookCreaterCvts, [FromHeader] string userId, [FromHeader] string token)
        {
            try
            {
                bool tkComparation = await _securityServices.Compare(userId, token);
                await _bookServices.CreateBooks(bookCreaterCvts);
                return Ok($"Tạo {bookCreaterCvts.Count()} sách mới thành công");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        #endregion Phần tạo sách 


        #region Class nhận Api
        public class ParamForBookCover
        {
            public int AmountCovers { get; set; }
            public List<string> SkipIds { get; set; } = new List<string>();
            [Required]
            public Dictionary<string, string> Fields { get; set; } = null!;
            private List<string> Letter(ParamForBookCover pbc)
            {
                return pbc.Fields.SelectMany(field => field.Value.Where(char.IsLetterOrDigit).Select(c => c.ToString())).ToList();
            }

            private List<string> Words(ParamForBookCover pbc, int amountLetter)
            {
                return pbc.Fields.SelectMany(field => Enumerable.Range(0, field.Value.Length - amountLetter + 1)
                    .Where(i => field.Value.Skip(i).Take(amountLetter).All(char.IsLetterOrDigit))
                    .Select(i => field.Value.Substring(i, amountLetter)))
                    .ToList();
            }

            public Dictionary<string, List<string>> FieldsToLetter()
            {
                return Fields.ToDictionary(f => f.Key, f => Letter(this));
            }
            public Dictionary<string, List<string>> FieldsToWords(int amountLetter)
            {
                return Fields.ToDictionary(f => f.Key, f => Words(this, amountLetter));
            }
        }
        #endregion Class nhận Api
    }
}

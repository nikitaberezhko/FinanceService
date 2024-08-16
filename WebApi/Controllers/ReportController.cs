using Asp.Versioning;
using AutoMapper;
using CommonModel.Contracts;
using FinanceService.Contracts.ApiModels;
using FinanceService.Contracts.Request;
using FinanceService.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Request;
using Services.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/reports")]
[ApiVersion(1)]
public class ReportController(
    IReportService reportService,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CommonResponse<GenerateReportResponse>>> Generate(
        GenerateReportRequest request)
    {
        var report = await reportService.Generate(mapper.Map<GenerateReportModel>(request));
        var result = new CreatedResult(
            nameof(Generate), new CommonResponse<GenerateReportResponse>
                { Data = mapper.Map<GenerateReportResponse>(report) });

        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CommonResponse<DeleteReportResponse>>> Delete(
        [FromRoute] DeleteReportRequest request)
    {
        var id = await reportService.Delete(mapper.Map<DeleteReportModel>(request));
        var result = new CommonResponse<DeleteReportResponse>
            { Data = new DeleteReportResponse { Id = id } };

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<CommonResponse<GetAllReportsResponse>>> GetAll(
        [FromQuery] GetAllReportsRequest request)
    {
        var reports = await reportService
            .GetShortenedList(mapper.Map<GetAllReportsModel>(request));
        var result = new CommonResponse<GetAllReportsResponse>
        {
            Data = new GetAllReportsResponse
                { Reports = mapper.Map<List<ShortenedReportApiModel>>(reports) }
        };

        return result;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommonResponse<GetReportByIdResponse>>> GetById(
        [FromRoute] GetReportByIdRequest request)
    {
        var report = await reportService.GetById(mapper.Map<GetReportByIdModel>(request));
        var result = new CommonResponse<GetReportByIdResponse>
            { Data = mapper.Map<GetReportByIdResponse>(report) };

        return result;
    }
}
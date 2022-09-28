using AutoMapper;
using BLL.DTOs.Dashboard;
using BLL.Mapping;
using DAL.Aggregates;
using System.Text.Json;

namespace BLL.Tests;

public class WidgetDashboardTest
{
    private readonly IMapper _mapper;

    public WidgetDashboardTest()
    {
        var mapperConfig = new MapperConfiguration(config => WidgetMapper.Configure(config));
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public void TestFromJsonMapper()
    {
        var exampleWidget = new Widget()
        {
            Configs = @"{
                ""VPN"": true,
                ""SSO"": false
            }"
        };

        var expectedWidgetDto = new WidgetDto
        {
            Configs = new Dictionary<string, object>()
            {
                {"VPN",true },
                {"SSO",false }
            }
        };

        var actualWidgetDto = _mapper.Map<WidgetDto>(exampleWidget);

        var actualVPN = (JsonElement)actualWidgetDto.Configs["VPN"];
        var actualSSO = (JsonElement)actualWidgetDto.Configs["SSO"];

        Assert.True(actualVPN.GetBoolean());
        Assert.False(actualSSO.GetBoolean());
    }

    [Fact]
    public void TestToJsonMapper()
    {
        var exampleWidgetDto = new WidgetDto
        {
            Configs = new Dictionary<string, object>()
            {
                {"SSO",true },
                {"VPN",false }
            }
        };

        var expectedWidget = new Widget
        {
            Configs = @"{""SSO"":true,""VPN"":false}"
        };

        var actualWidget = _mapper.Map<Widget>(exampleWidgetDto);

        Assert.Equal(expectedWidget.Configs, actualWidget.Configs);
    }

    [Fact]
    public void TestConvertDashboardDtoToDashboard()
    {
        var exampleWidgetDto = new WidgetDto
        {
            Configs = new Dictionary<string, object>()
            {
                {"SSO",true },
                {"VPN",false }
            }
        };
        var exampleWidgetDto2 = new WidgetDto
        {
            Configs = new Dictionary<string, object>()
            {
                {"SSO",true },
                {"VPN",false }
            }
        };
        var exampleWidgetDto3 = new WidgetDto
        {
            Configs = new Dictionary<string, object>()
            {
                {"SSO",true },
                {"VPN",false }
            }
        };

        var exampleDashboardDto = new DashboardDto
        {
            Id = Guid.NewGuid(),
            Widgets = new List<WidgetDto>()
            {
                exampleWidgetDto, exampleWidgetDto2, exampleWidgetDto3
            }
        };

        var exampleDashboard = _mapper.Map<Dashboard>(exampleDashboardDto);

        Assert.True(exampleDashboard is not null);
    }
}
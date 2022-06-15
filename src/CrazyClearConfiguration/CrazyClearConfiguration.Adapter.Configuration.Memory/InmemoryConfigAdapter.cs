using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using CrazyClearConfiguration.Core;

namespace CrazyClearConfiguration.Adapter.Configuration.Memory;

public class InmemoryConfigAdapter : IConfigPort
{
    public Task<ExpandoObject> Read()
    {
        var data = @"{
            'ApplicationSettings': {
                'Sources': {
                    'default': {
                        'ApiSettings': {
                            'LeadService': {
                                'BaseUrl': '',
                                'ApiKey': null,
                                'IsTest': true
                            }
                        },
                        'ClientSettings': {
                            'LegacySource': 'customer',
                            'Redirects': [
                                {
                                    'source': 'customer',
                                    'from': 'huisscan',
                                    'to': 'isolatie'
                                }
                            ],              
                            'ShowProductTips': true,
                            'CanAccessLandingPage': true,
                            'PrePaymentEnabled': false,
                            'CanRefineProducts': true,
                            'RefinementBackButtonTo': 'Advice',
                            'PdfQuoteForHappyFlowProducts': [
                                'SolarPanel',
                                'CavityWallInsulation',
                                'FloorInsulation',
                                'Heatpump',
                                'RoofInsulation'
                            ],
                            'PdfQuoteForSemihappyFlowProducts': [
                                'FloorInsulation',
                                'CavityWallInsulation',
                                'SolarPanel',
                                'Heatpump',
                                'RoofInsulation'
                            ],
                            'PdfQuoteForImpossibleProducts': [ '' ],
                            'PdfQuoteToTransferProducts': [ '' ],
                            'ABTestEnabledProducts': [
                                'SolarPanel',
                                'CavityWallInsulation',
                                'FloorInsulation',
                                'Boiler'
                            ],
                            'RefinementRemarkProducts': [
                                'SolarBoiler',
                                'RoofInsulation',
                                'Glass',
                                'Boiler',
                                'Radiator',
                                'OutsideWallInsulation',
                                'FloorHeating',
                                'CombinationHeatpump',
                                'VentilationHeatpump',
                                'AllElectricHeatpump'
                            ],
                            'AdviceReportExtended': false,
                            'Tealium': {
                                'Environment': 'dev'
                            },
                            'AppointmentRequest': {
                                'Enabled': false,
                                'PostUrl': '',
                                'Source': '',
                                'Reference': ''
                            },
                            'SolarPanel': {
                                'MinPanelCountForPreferences': 6
                            },
                            'GoogleTagManager': {
                                'id': '',
                                'enabled': false,
                                'SourceContainers': {
                                    'customer': ''
                                }
                            },
                            'SourceOverrides': {
                                'customer1': {
                                    'CustomerPdfReportEnabled': false
                                }
                            },
                            'InitialSolarCategoryId': 0
                        },
                        'ProductSettings': {
                            'SolarPanelSettings': {
                                'InverterName:InverterSingleMpp': 'test'
                            },
                            'FloorInsulationSettings': {
                                'DefaultProductIds': [
                                    6,
                                    10
                                ],
                                'LowHeightProductIds': [
                                    9,
                                    11
                                ]
                            },
                            'RoofInsulationSettings': {
                                'FinishedFlatProductId': 0,
                                'UnfinishedFlatProductId': 0,
                                'FinishedSlopedProductId': 10,
                                'UnfinishedSlopedProductId': 8
                            }
                        }
                    },
                    'customer': {
                        'ClientSettings': {
                            'LegacySource': 'customer'
                        }
                    }
                },
                'Flows': {
                }
            },
            'PromotionSettings': {
                'JwtRsaPublicKey': 'this_is_a_fake_key'
            },
            'AzureAppConfiguration': {
                'Disabled': true,
                'ConnectionString': 'test',
                'EnvironmentLabel': 'fake'
            },
            'Serilog': {
                'MinimumLevel': {
                    'Default': 'Information',
                    'Override': {
                        'Microsoft': 'Warning',
                        'Microsoft.Hosting.Lifetime': 'Information'
                    }
                }
            },
            'AllowedHosts': '*',
            'Jwt': {
                'Issuer': 'http://localhost:5032/',
                'Audience': 'test',
                'Key': 'this_is_a_fake_key',
                'LifeTime': 120
            }
        }";
        return Task.FromResult(JsonSerializer.Deserialize<ExpandoObject>(data) ?? new ExpandoObject());
    }

    public Task Write(ExpandoObject config)
    {
        throw new NotImplementedException();
    }
}
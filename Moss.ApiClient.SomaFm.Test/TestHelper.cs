﻿using System.Net;

namespace Moss.ApiClient.SomaFm.Test;

internal static class TestHelper
{
    internal static Mock<HttpMessageHandler> CreateHttpMessageHandlerMock(HttpStatusCode statusCode, string requestUri, HttpContent content)
    {
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri(requestUri)), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content
            });

        return handlerMock;
    }
}

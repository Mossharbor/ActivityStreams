using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    //using System;
    //using System.Net;
    //using System.Net.Http;
    //using System.Threading;
    //using System.Threading.Tasks;

    ///// <summary>
    ///// This is helper class for mocking http client requests.
    ///// </summary>
    //public class DelegatingHandlerStub : DelegatingHandler
    //{
    //    /// <summary>
    //    /// THe handler function
    //    /// </summary>
    //    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DelegatingHandlerStub"/> class.
    //    /// </summary>
    //    public DelegatingHandlerStub()
    //    {
    //        this.handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DelegatingHandlerStub"/> class.
    //    /// </summary>
    //    /// <param name="handlerFunc">the handler function for the request</param>
    //    public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
    //    {
    //        this.handlerFunc = handlerFunc;
    //    }

    //    /// <summary>
    //    /// Sends the response
    //    /// </summary>
    //    /// <param name="request">teh request</param>
    //    /// <param name="cancellationToken">the token</param>
    //    /// <returns>the task</returns>
    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        return this.handlerFunc(request, cancellationToken);
    //    }
    //}
}

namespace Microsoft.Examples.V4.Controllers
{
    using AspNetCore.Routing;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a RESTful people service.
    /// </summary>
    [ApiVersion( "4.0" )]
    [Route( "api/v{api-version:apiVersion}/[controller]" )]
    public class PeopleController : Controller
    {
        const string ByIdRouteName = "GetPersonById" + nameof( V4 );

        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns>All available people.</returns>
        /// <response code="200">The successfully retrieved people.</response>
        [HttpGet]
        [ProducesResponseType( typeof( IEnumerable<Person> ), 200 )]
        public IActionResult Get()
        {
            var people = new[]
            {
                new Person()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@somewhere.com",
                    Phone = "555-987-1234",
                    DateofBirth = new System.DateTime(1979, 2, 12)
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob.smith@somewhere.com",
                    Phone = "555-654-4321",
                    DateofBirth = new System.DateTime(1989, 7, 3)
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@somewhere.com",
                    Phone = "555-789-3456",
                    DateofBirth = new System.DateTime(1987, 9, 2)
                },
                new Person()
                {
                    Id = 4,
                    FirstName = "Hakan",
                    LastName = "Tuncer",
                    Email = "hakan.tuncer@somewhere.com",
                    Phone = "555-643-8432",
                    DateofBirth = new System.DateTime(1982, 8, 9)
                }
            };

            return Ok( people );
        }

        /// <summary>
        /// Gets a single person.
        /// </summary>
        /// <param name="id">The requested person identifier.</param>
        /// <returns>The requested person.</returns>
        /// <response code="200">The person was successfully retrieved.</response>
        /// <response code="404">The person does not exist.</response>
        [HttpGet( "{id:int}", Name = ByIdRouteName )]
        [ProducesResponseType( typeof( Person ), 200 )]
        [ProducesResponseType( 404 )]
        public IActionResult Get( int id ) =>
            Ok( new Person()
                {
                    Id = id,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@somewhere.com",
                    Phone = "555-987-1234",
                    DateofBirth = new System.DateTime(1979, 2, 12)
                }
            );

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="person">The person to create.</param>
        /// <returns>The created person.</returns>
        /// <response code="201">The person was successfully created.</response>
        /// <response code="400">The person was invalid.</response>
        [HttpPost]
        [ProducesResponseType( typeof( Person ), 201 )]
        [ProducesResponseType( 400 )]
        public IActionResult Post( [FromBody] Person person )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            person.Id = 42;

            return CreatedAtRoute( ByIdRouteName, new { id = person.Id }, person );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Battleship_API.Models;
using BattleshipClass;

namespace Battleship_API.Controllers
{
    public class GameApiController : ApiController
    {
        [HttpPost]
        [Route("api/game")]
        public IHttpActionResult NewGame()
        {           
            Game game = new Game();
            ActiveGames.Instance.listOfGames.Add(game);

            return Ok(game);
        }

        [HttpGet]
        [Route("api/game")]
        public IHttpActionResult GetGame(BattleshipModel model)
        { 
            return Ok(ActiveGames.Instance.listOfGames[model.Index]);
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/game")]   
        public IHttpActionResult MakeMove(BattleshipModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.input)
                {
                    case Input.MakeMove:
                        var result = ActiveGames.Instance.listOfGames[model.Index].m_playerTwo.MakeMove(model.Row, model.Col);

                        switch (result)
                        {
                            case SquareType.Water:
                                ActiveGames.Instance.listOfGames[model.Index].m_playerOne.AiMakeMove
                                    (ActiveGames.Instance.listOfGames[model.Index].m_playerOne.m_squares);
                                break;

                            case SquareType.Boat:
                                break;

                            case SquareType.WaterHit:
                                break;

                            case SquareType.BoatHit:
                                break;
                        }
                        return Ok(ActiveGames.Instance.listOfGames[model.Index].m_playerTwo);

                    case Input.Reset:
                        if (ActiveGames.Instance.listOfGames[model.Index] != null)
                        {
                            ActiveGames.Instance.listOfGames[model.Index].m_playerOne.Reset();
                            ActiveGames.Instance.listOfGames[model.Index].m_playerTwo.Reset();
                            ActiveGames.Instance.listOfGames[model.Index].m_playerTwo.RandomBoatPlacements();

                            return Ok(ActiveGames.Instance.listOfGames[model.Index]);
                        }
                        else
                            return NotFound();

                    case Input.PlaceBoat:
                        //if True place horizontal, else place vertical
                        if (model.Direction)
                        {
                            if (model.Col + model.BoatSize <= 9/* && model.Col - model.BoatSize >= 0*/)
                                ActiveGames.Instance.listOfGames[model.Index].m_playerOne.HorizontalPlacement(model.ID, model.BoatSize, model.Row, model.Col);
                            else
                                return BadRequest("Out of bounds");
                        }

                        else
                        {
                            if (model.Row + model.BoatSize <= 9/* && model.Row - model.BoatSize >= 0)*/)
                                ActiveGames.Instance.listOfGames[model.Index].m_playerOne.VerticalPlacement(model.ID, model.BoatSize, model.Row, model.Col);
                            else
                                return BadRequest("Out of bounds");
                        }
                        break;
                }
                return Ok(ActiveGames.Instance.listOfGames[model.Index]);
            }
            else
                return BadRequest(ModelState);
        }
    }
}

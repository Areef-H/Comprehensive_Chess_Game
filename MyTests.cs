using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Cecs475.BoardGames.Chess.Model;

namespace Cecs475.BoardGames.Chess.Test
{	
	
	public class MyTests : ChessTest
	{
		/* This is where you will write your tests.
		 * Each test must be marked with the [Test] attribute.
		 *
		 * Double check that you follow these rules:
		 *
		 * 1. Every test method should have a meaningful name.
		 * 2. Every Should() must include a string description of the expectation.
		 * 3. Your buster test should be LAST in this file, and should be given a meaningful name
		 *		FOLLOWED BY AN UNDERSCORE, followed by the LAST 6 digits of your student ID.
		 *		Example:
		 *
		 *		If my ID is 012345678 and involves undoing a castling move, my test might be named
		 *		UndoCastleQueenSide_345678
		 *
		 */
		[Fact]
		public void rook_validator()
		{
			ChessBoard board = new ChessBoard();
			board.GetPieceAtPosition(Pos(0, 0)).PieceType.Should().Be(ChessPieceType.Rook, "A Rook should be at 0,0 ");
			
		}
		
		[Fact]
		public void pawn_mv_validator()
		{
			ChessBoard board = new ChessBoard();
			
			var possMoves = board.GetPossibleMoves();
			var pawnPosition = Pos(6, 1);
			var pawnMoves = GetMovesAtPosition(possMoves, pawnPosition);
			pawnMoves.Should().HaveCount(2, "The pawn should have 2 moves"); 
			
		}

		[Fact] public void Undo_Player_switch()
		{
			ChessBoard b = CreateBoardFromPositions(
				Pos("b8"), ChessPieceType.King, 2,
				Pos("a1"), ChessPieceType.King, 1
					
					
			);
			Apply(b, "a1, a2"); //A white piece is moving
			Apply(b, "b8, b7"); //A white piece is moving

			b.UndoLastMove();
			b.CurrentPlayer.Should().Be(2, "When you are undoing something it should have the original player");
			
		}
		
		
		[Fact]
		public void player_switch_validator()
		{
				ChessBoard b = CreateBoardFromPositions(
					Pos("b8"), ChessPieceType.King, 2,
					Pos("a1"), ChessPieceType.King, 1
					
					
				);
				
			
				Apply(b, "a1, a2"); 
				var possMoves = b.GetPossibleMoves();
				b.CurrentPlayer.Should().Be(2, "Player 1 Moving a piece should transition to player 2' turn");
			}
		[Fact]
		public void Advantage_check()
		{
			ChessBoard b = CreateBoardFromPositions(
				Pos("a8"), ChessPieceType.King, 2,
				Pos("g8"), ChessPieceType.Bishop,2, 
				Pos("a2"), ChessPieceType.Pawn,1 ,
				Pos("g1"), ChessPieceType.King,1
			);
			b.CurrentAdvantage.Should().Be(Advantage(2, 2), "Player 2 has a bishop and a King,while Player 1 only has a king and a pawn");


			
		}
		
		//tricky
		[Fact]
		public void Castling_Validator()
		{
			var b = CreateBoardFromMoves(
				"e2, e4", 
				"e7, e5", 
				"g1, f3",
				"g8, f6", 
				"f1, e2", 
				"f8, e7");

			
			b.ApplyMove(Move("e1, g1")); 
			b.ApplyMove(Move("h1, f1"));
			
		
			var kingPosition = b.GetPieceAtPosition(Pos("g1"));
			var rookPosition = b.GetPieceAtPosition(Pos("f1"));
			
			kingPosition.PieceType.Should().Be(ChessPieceType.King, "The piece was moved to g1");
			rookPosition.PieceType.Should().Be(ChessPieceType.Rook, "the piece was moved to f1");
	
		
		}
		//buster
		[Fact]
		public void Knight_movement()
		{
			ChessBoard b = CreateBoardFromPositions(
				Pos("a8"), ChessPieceType.King, 2,
				Pos("g8"), ChessPieceType.Pawn,2, 
				Pos("a2"), ChessPieceType.Knight,1 ,
				Pos("g1"), ChessPieceType.King,1
			);
			
			Apply(b, "a2, b4"); 
			b.GetPieceAtPosition(Pos("b4")).Player.Should().Be(1, "Player1 moved their knight there on b4");
			b.UndoLastMove();
			Apply(b, "a2, c1"); 
			b.GetPieceAtPosition(Pos("c1")).Player.Should().Be(1, "Player1 moved their knight there on c1");
			
		}

			
			
		}
	
	}
		
	
		
		
			








		
	


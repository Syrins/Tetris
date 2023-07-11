using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
	public class TetrisField
	{
		public int TilesWidth { get; private set; }
		public int TilesHeight { get; private set; }
		
		private TileType[,] Tiles;
		
		public Color BackColor, BorderColor;
		
		public TileType this[int row, int col]
		{
			get
			{
				try
				{
					return Tiles[row, col];
				}
				catch(IndexOutOfRangeException)
				{
					return TileType.Wall;
				}
			}
		}
		
		
		public TetrisField(int height, int width)
		{
			TilesWidth=width;
			TilesHeight=height;
			Tiles=new TileType[height, width];
			
			BorderColor=Color.FromArgb(192, 192, 192);
			BackColor=Color.FromArgb(240, 240, 240);
			
			for(int row=0; row<TilesHeight; row++)
			{
				for(int col=0; col<TilesWidth; col++)
				{
					Tiles[row, col]=TileType.Empty;
				}
			}
		}
		
		
		
		public bool SetCell(int row, int col, TileType type)
		{
			try
			{
				Tiles[row, col]=type;
				return true;
			}
			catch(IndexOutOfRangeException)
			{
				return false;
			}
		}
		
		public int SetFigure(Figure f, bool rewrite)
		{
			
			int res=4;
			try
			{
				if(Tiles[f.YC, f.XC]==TileType.Empty || rewrite)
				{
					Tiles[f.YC, f.XC]=f.Type;
				}
				else --res;
			}
			catch(IndexOutOfRangeException)
			{ --res; }
			
			
			try
			{
				if(Tiles[f.Y1, f.X1]==TileType.Empty || rewrite)
				{
					Tiles[f.Y1, f.X1]=f.Type;
				}
				else --res;
			}
			catch(IndexOutOfRangeException)
			{ --res; }
			
			try
			{
				if(Tiles[f.Y2, f.X2]==TileType.Empty || rewrite)
				{
					Tiles[f.Y2, f.X2]=f.Type;
				}
				else --res;
			}
			catch(IndexOutOfRangeException)
			{ --res; }
			
			try
			{
				if(Tiles[f.Y3, f.X3]==TileType.Empty || rewrite)
				{
					Tiles[f.Y3, f.X3]=f.Type;
				}
				else --res;
			}
			catch(IndexOutOfRangeException)
			{ --res; }
			
			return res;
		}
		public bool IsEmpty(Figure f)
		{
			if(this[f.YC, f.XC]!=TileType.Empty) return false;
			if(this[f.Y1, f.X1]!=TileType.Empty) return false;
			if(this[f.Y2, f.X2]!=TileType.Empty) return false;
			if(this[f.Y3, f.X3]!=TileType.Empty) return false;
			return true;
		}
		public bool IsEmpty(int row, int col)
		{
			if(this[row, col]!=TileType.Empty) return false;
			return true;
		}
		protected void EraseFigure(Figure f)
		{
			f.Type=TileType.Empty;
			SetFigure(f, true);
		}
		


		/// <param name="row"
		/// <param name="col"
		protected bool MoveDown(int row, int col)
		{
			if(Tiles[row, col]!=TileType.Empty)
			{
				TileType below=Tiles[row+1, col];
				if(below==TileType.Empty)
				{
					Tiles[row+1, col]=Tiles[row, col];
					Tiles[row, col]=TileType.Empty;
				}
				return Tiles[row+2, col]==TileType.Empty;
			}
			return false;
		}

		/// <param name="f"
		protected bool MoveDown(Figure f)
		{
			Figure lower=f.MoveDown();
			
			f.Type=TileType.Empty;
			SetFigure(f, true);
			
			if(IsEmpty(lower))
			{
				SetFigure(lower, false);
				return true;
			}
			else
			{
				f.Type=lower.Type;
				SetFigure(f, false);
				return false;
			}
		}
		
		protected bool CanMoveDown(int row, int col)
		{
			return Tiles[row+1, col]==TileType.Empty;
		}
		
		protected bool CanMoveDown(Figure f)
		{
			Figure lower=f.MoveDown();
			
			f.Type=TileType.Empty;
			SetFigure(f, true);
			
			bool able=IsEmpty(lower);
			
			f.Type=lower.Type;
			SetFigure(f, false);
			
			return able;
			
		}
		

		/// <param name="row"
		/// <param name="col"

		protected bool MoveRight(int row, int col)
		{
			if(Tiles[row, col]!=TileType.Empty)
			{
				TileType below=Tiles[row, col+1];
				if(below==TileType.Empty)
				{
					Tiles[row, col+1]=Tiles[row, col];
					Tiles[row, col]=TileType.Empty;
				}
				return Tiles[row, col+1]==TileType.Empty; 
			}
			return false;
		}

		/// <param name="f"

		protected bool MoveRight(Figure f)
		{
			Figure moved=f.MoveRight();
			f.Type=TileType.Empty;
			SetFigure(f, true);
			if(IsEmpty(moved))
			{
				SetFigure(moved, false);
				return true;
			}
			f.Type=moved.Type;
			SetFigure(f, false);
			return false;
		}
		
		protected bool CanMoveRight(int row, int col)
		{
			return Tiles[row, col+1]==TileType.Empty;
		}
		protected bool CanMoveRight(Figure f)
		{
			return CanMoveRight(f.YC, f.XC) && CanMoveRight(f.Y1, f.X1)
				 && CanMoveRight(f.Y2, f.X2) && CanMoveRight(f.Y3, f.X3);
		}



		/// <param name="row"
		/// <param name="col"
		protected bool MoveLeft(int row, int col)
		{
			if(Tiles[row, col]!=TileType.Empty)
			{
				TileType below=Tiles[row, col-1];
				if(below==TileType.Empty)
				{
					Tiles[row, col-1]=Tiles[row, col];
					Tiles[row, col]=TileType.Empty;
				}
				return Tiles[row, col-1]==TileType.Empty; 
			}
			return false;
		}

		/// <param name="f">

		protected bool MoveLeft(Figure f)
		{
			Figure moved=f.MoveLeft();
			f.Type=TileType.Empty;
			SetFigure(f, true);
			if(IsEmpty(moved))
			{
				SetFigure(moved, false);
				return true;
			}
			f.Type=moved.Type;
			SetFigure(f, false);
			return false;
		}
		
		public bool CanMoveLeft(int row, int col)
		{
			return Tiles[row, col+1]==TileType.Empty;
		}
		public bool CanMoveLeft(Figure f)
		{
			return CanMoveLeft(f.YC, f.XC) && CanMoveLeft(f.Y1, f.X1)
				 && CanMoveLeft(f.Y2, f.X2) && CanMoveLeft(f.Y3, f.X3);
		}
		
		protected Figure RotateFigure(Figure f)
		{
			Figure rotated=f.Rotate(), rotated2;
			f.Type=TileType.Empty;
			SetFigure(f, true);
			f.Type=rotated.Type;
			
			if(IsEmpty(rotated))
			{
				SetFigure(rotated, false);
				return rotated;
			}
			rotated2=rotated.MoveDown();
			if(IsEmpty(rotated2))
			{
				SetFigure(rotated2, false);
				return rotated2;
			}
			rotated2=rotated.MoveRight();
			if(IsEmpty(rotated2))
			{
				SetFigure(rotated2, false);
				return rotated2;
			}
			rotated2=rotated.MoveLeft();
			if(IsEmpty(rotated2))
			{
				SetFigure(rotated2, false);
				return rotated2;
			}
			SetFigure(f, false);
			return Figure.Zero;
		}
		
		public int RemoveFullRows()
		{
			List<int> FullRows=new List<int>();
			
			for(int row=0; row<TilesHeight; row++)
			{
				bool fullrow=true;
				for(int col=0; col<TilesWidth; col++)
				{
					if(Tiles[row, col]==TileType.Empty)
					{
						fullrow=false;
						break;
					}
				}
				if(fullrow)
				{
					FullRows.Add(row);
				}
			}
			
			foreach(int frow in FullRows)
			{
				for(int row=frow-1; row>0; row--)
				{
					for(int col=0; col<TilesWidth; col++)
					{
						Tiles[row+1, col]=Tiles[row, col];
						if(IsRowEmpty(row+1)) 
							break;
					}
				}
			}
			
			return TilesWidth*FullRows.Count; 
		}
		
		private bool IsRowEmpty(int row)
		{
			for(int col=0; col<TilesWidth; col++)
			{
				if(Tiles[row, col]!=TileType.Empty)
					return false;
			}
			return true;
		}
		
		public virtual void Clear()
		{
			for(int row=0; row<TilesHeight; row++)
			{
				for(int col=0; col<TilesWidth; col++)
				{
					SetCell(row, col, TileType.Empty);
				}
			}
		}
		
		
		public const int TileSide=20;
		
		public virtual void Paint(Graphics g)
		{
			Pen border=new Pen(BorderColor, 2F);
			SolidBrush fone=new SolidBrush(BackColor);
			
			g.DrawRectangle(border, 4, 4, TilesWidth*TileSide+2, TilesHeight*TileSide+2);
			g.FillRectangle(fone, 5, 5, TilesWidth*TileSide, TilesHeight*TileSide);
			
			for(int row=0; row<TilesHeight; row++)
			{
				for(int col=0; col<TilesWidth; col++)
				{
					Rectangle tile=new Rectangle(5+col*TileSide, 5+row*TileSide, TileSide, TileSide);
					switch(Tiles[row, col])
					{
						case TileType.Blue:
							if(Blue==null) g.FillRectangle(Brushes.Blue, tile);
							else g.DrawImage(Blue, tile);
							break;
						case TileType.Green:
							if(Green==null) g.FillRectangle(Brushes.Green, tile);
							else g.DrawImage(Green, tile);
							break;
						case TileType.Yellow:
							if(Yellow==null) g.FillRectangle(Brushes.Yellow, tile);
							else g.DrawImage(Yellow, tile);
							break;
						case TileType.Purple:
							if(Purple==null) g.FillRectangle(Brushes.Purple, tile);
							else g.DrawImage(Purple, tile);
							break;
						case TileType.Orange:
							if(Orange==null) g.FillRectangle(Brushes.Orange, tile);
							else g.DrawImage(Orange, tile);
							break;
						case TileType.Red:
							if(Red==null) g.FillRectangle(Brushes.Red, tile);
							else g.DrawImage(Red, tile);
							break;
						case TileType.LightBlue:
							if(LightBlue==null) g.FillRectangle(Brushes.LightBlue, tile);
							else g.DrawImage(LightBlue, tile);
							break;
					}
				}
			}
		}
		public static Bitmap Red, Green, Blue, Yellow, Orange, Purple, LightBlue;
	}
	
	
	public class GameField: TetrisField
	{
		public Figure Current;

		public bool IsFigureFalling { get; private set; }
		public bool ShowTips;
		
		
		public GameField(int height, int width): base(height, width)
		{
			IsFigureFalling=false;
			ShowTips=true;
			Current=Figure.Zero;
		}
		
		


		/// <param name="f"

		public bool PlaceFigure(Figure f)
		{
			f=f.MoveTo(0, TilesWidth/2-1);
			int scs=SetFigure(f, false);
			Current=f;
			if(scs!=4)
				return false;
			IsFigureFalling=true;
			return true;
		}
		
		/// <param name="nfig"

		public Figure ChangeFigure(Figure nfig)
		{
			if(Current==Figure.Zero) return Current;
			Figure old = Current;
			EraseFigure(old);
			if(!PlaceFigure(nfig))
				return Figure.Zero;
			return old;
		}
		

		public bool RotateFigure()
		{
			if(Current==Figure.Zero) return false;
			Figure t=RotateFigure(Current);
			if(t!=Figure.Zero)
			{
				Current=t;
				return true;
			}
			return false;
		}

		public bool MoveLeft()
		{
			if(Current==Figure.Zero) return false;
			if(MoveLeft(Current))
			{
				Current=Current.MoveLeft();
				return true;
			}
			return false;
		}

		public bool MoveRight()
		{
			if(Current==Figure.Zero) return false;
			if(MoveRight(Current))
			{
				Current=Current.MoveRight();
				return true;
			}
			return false;
		}

		public bool MoveDown()
		{
			if(Current==Figure.Zero) return false;
			if(MoveDown(Current))
			{
				Current=Current.MoveDown();
				return true;
			}
			return false;
		}

		public bool Drop()
		{
			if(Current==Figure.Zero) return false;
			while(Current!=Figure.Zero)
				DoStep();
			return true;
		}
		
		public void DoStep()
		{
			if(Current!=Figure.Zero)
			{
				IsFigureFalling=MoveDown(Current);	
				if(IsFigureFalling)	//успех?
				{
					Current=Current.MoveDown(); 
				}
				else
					Current=Figure.Zero;
			}
			else
				IsFigureFalling=false;
		}

		public override void Clear()
		{
			base.Clear();
			
			Current=Figure.Zero;
			IsFigureFalling=false;
		}
		
		public override void Paint(Graphics g)
		{
			base.Paint(g);
			
			if(ShowTips && IsFigureFalling)
			{
				Figure tip=Current;
				EraseFigure(Current);
				
				while(IsEmpty(tip)) 
				{
					tip=tip.MoveDown();
				}

				tip=tip.MoveUp();
				
				SetFigure(Current, false);
				
				Point[] cells=new Point[]
				{
					new Point(tip.XC, tip.YC), new Point(tip.X1, tip.Y1),
					new Point(tip.X2, tip.Y2), new Point(tip.X3, tip.Y3)
				};
				
				SolidBrush b=new SolidBrush(Color.FromArgb(32, 192, 0, 0));
				Pen p=new Pen(Color.FromArgb(128, 192, 0, 0), 1);
				
				foreach(Point cell in cells)
				{
					if(!IsEmpty(cell.Y, cell.X)) continue;
					Rectangle tile = new Rectangle(6+cell.X*TileSide, 6+cell.Y*TileSide, TileSide-2, TileSide-2);
					g.FillRectangle(b, tile);
				}
			}
		}
	}
	
	
	public struct Figure
	{
		public int XC { get; private set; }
		public int YC { get; private set; }
		
		public int X1 { get; private set; }
		public int Y1 { get; private set; }
		
		public int X2 { get; private set; }
		public int Y2 { get; private set; }
		
		public int X3 { get; private set; }
		public int Y3 { get; private set; }
		
		public TileType Type;
		
		public static readonly Figure Zero=new Figure(TileType.Empty);
		
		
		public Figure(TileType type):this()
		{
			Type=type;
			XC=0;
			YC=0;
			switch(type)
			{
				case TileType.Blue: // I
					X1=XC-1; X2=XC+1; X3=XC+2;
					Y1=YC; Y2=YC; Y3=YC;
					break;
				case TileType.LightBlue: // L
					X1=XC-1; X2=XC-1; X3=XC+1;
					Y1=YC+1; Y2=YC; Y3=YC;
					break;
				case TileType.Green: // Z
					X1=XC-1; X2=XC; X3=XC+1;
					Y1=YC; Y2=YC+1; Y3=YC+1;
					break;
				case TileType.Orange: // Г
					X1=XC-1; X2=XC+1; X3=XC+1;
					Y1=YC; Y2=YC; Y3=YC+1;
					break;
				case TileType.Purple: // T
					X1=XC-1; X2=XC; X3=XC+1;
					Y1=YC; Y2=YC+1; Y3=YC;
					break;
				case TileType.Red: // S
					X1=XC-1; X2=XC; X3=XC+1;
					Y1=YC+1; Y2=YC+1; Y3=YC;
					break;
				case TileType.Yellow: // [ ]
					X1=XC+1; X2=XC; X3=XC+1;
					Y1=YC; Y2=YC+1; Y3=YC+1;
					break;
				case TileType.Empty: // zero
					X3=X2=X1=XC=0;
					Y3=Y2=Y1=YC=0;
					break;
				default:
					X3=X2=X1=XC=0;
					Y3=Y2=Y1=YC=0;
					break;
			}
		}
		
		public Figure MoveDown()
		{
			return MoveTo(YC+1, XC);
		}
		public Figure MoveUp()
		{
			return MoveTo(YC-1, XC);
		}
		
		public Figure MoveRight()
		{
			return MoveTo(YC, XC+1);
		}
		
		public Figure MoveLeft()
		{
			return MoveTo(YC, XC-1);
		}
		
		public Figure MoveTo(int row, int col)
		{
			int dx=col-XC, dy=row-YC;
			Figure res=new Figure(this.Type);
			res.XC=col; res.YC=row;
			res.X1=X1+dx; res.Y1=Y1+dy;
			res.X2=X2+dx; res.Y2=Y2+dy;
			res.X3=X3+dx; res.Y3=Y3+dy;
			return res;
		}
		
		private int RotateCol(int col)
		{
			return YC-XC+col;
		}
		private int RotateRow(int row)
		{
			return XC-row+YC;
		}

		public Figure Rotate()
		{
			Figure res=Clone();
			res.X1=RotateRow(Y1); res.Y1=RotateCol(X1);
			res.X2=RotateRow(Y2); res.Y2=RotateCol(X2);
			res.X3=RotateRow(Y3); res.Y3=RotateCol(X3);
			return res;
		}
				
		public static bool operator ==(Figure f1, Figure f2)
		{
			return f1.Type==f2.Type && f1.XC==f2.XC && f1.YC==f2.YC &&
				f1.X1==f2.X1 && f1.X2==f2.X2 && f1.X3==f2.X3 && 
				f1.Y1==f2.Y1 && f1.Y2==f2.Y2 && f1.Y3==f2.Y3;
		}
		public static bool operator !=(Figure f1, Figure f2)
		{
			return !(f1==f2);
		}
		
		private Figure Clone()
		{
			Figure res=new Figure(this.Type);
			res.XC=XC; res.YC=YC; res.X1=X1; res.Y1=Y1;
			res.X2=X2; res.Y2=Y2; res.X3=X3; res.Y3=Y3;
			return res;
		}
		
		
		private static Random rnd=new Random();
		public static Figure RandomFigure()
		{
			return new Figure((TileType)rnd.Next(1, 8));
		}
	}
	
	public enum TileType { Empty, Red, Green, Blue, Yellow, Orange, Purple, LightBlue, Wall }
}

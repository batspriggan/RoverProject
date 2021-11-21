using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoverTestBench
{
    [TestClass]
    public class TestRover
    {
        ISurface? surface;
        [TestInitialize]
        public void Init()
        { 
            surface = new Planet(100,100);
            surface.AddObstacle(50, 50);
        }

        [TestMethod]
        public void MoveForwardHorz()
        {
            Rover rover = new Rover(surface!);
            rover.CurrentPosition = (10, 10, Direction.E);
            rover.MoveForward();
            Assert.AreEqual((11, 10, Direction.E), rover.CurrentPosition);

            rover.CurrentPosition = (10, 10, Direction.W);
            rover.MoveForward();
            Assert.AreEqual((9, 10, Direction.W), rover.CurrentPosition);
        }
        [TestMethod]
        public void MoveForwardVert()
        {
            IRover<ISurface> rover = new Rover(surface!);
            rover.CurrentPosition = (10, 10, Direction.N);
            rover.MoveForward();
            Assert.AreEqual((10, 11, Direction.N), rover.CurrentPosition);

            rover.CurrentPosition = (10, 10, Direction.S);
            rover.MoveForward();
            Assert.AreEqual((10, 9, Direction.S), rover.CurrentPosition);
        }

        [TestMethod]
        public void MoveBackwardVert()
        {
            IRover<ISurface> rover = new Rover(surface!);
            rover.CurrentPosition = (10, 10, Direction.N);
            rover.MoveBackward();
            Assert.AreEqual((10, 9, Direction.N), rover.CurrentPosition);

            rover.CurrentPosition = (10, 10, Direction.S);
            rover.MoveBackward();
            Assert.AreEqual((10, 11, Direction.S), rover.CurrentPosition);
        }

        [TestMethod]
        public void MoveBackwardHorz()
        {
            var rover = new Rover(surface!);
            rover.CurrentPosition = (10, 10, Direction.E);
            rover.MoveBackward();
            Assert.AreEqual( (9, 10, Direction.E), rover.CurrentPosition);
            rover.CurrentPosition = (10, 10, Direction.W);
            rover.MoveBackward();
            Assert.AreEqual( (11, 10, Direction.W), rover.CurrentPosition);
        }

        [TestMethod]
        public void MixedConsecutiveMove()
        {
            var rover = new Rover(surface!);
            rover.CurrentPosition = (10, 10, Direction.E);
            rover.MoveForward();
            rover.MoveForward();
            Assert.AreEqual((12, 10, Direction.E), rover.CurrentPosition);
            rover.TurnRight();
            Assert.AreEqual((12, 10, Direction.S), rover.CurrentPosition);
            rover.MoveForward();
            rover.MoveForward();
            Assert.AreEqual((12, 8, Direction.S), rover.CurrentPosition);
            rover.TurnRight();
            Assert.AreEqual((12, 8, Direction.W), rover.CurrentPosition);
            rover.TurnRight();
            Assert.AreEqual((12, 8, Direction.N), rover.CurrentPosition);
            rover.TurnLeft();
            Assert.AreEqual((12, 8, Direction.W), rover.CurrentPosition);
            rover.TurnLeft();
            Assert.AreEqual((12, 8, Direction.S), rover.CurrentPosition);
            rover.TurnLeft();
            Assert.AreEqual((12, 8, Direction.E), rover.CurrentPosition);
            rover.TurnLeft();
            Assert.AreEqual((12, 8, Direction.N), rover.CurrentPosition);
        }

        [TestMethod]
        public void TestBorders()
        {
            var rover = new Rover(surface!);
            rover.CurrentPosition = (0, 0, Direction.S);
            rover.MoveForward();
            Assert.AreEqual((0, surface!.Width, Direction.S), rover.CurrentPosition);
            rover.TurnRight();
            rover.MoveForward();
            Assert.AreEqual((surface!.Length, surface!.Width, Direction.W), rover.CurrentPosition);
            rover.TurnRight();
            rover.MoveForward();
            Assert.AreEqual((surface!.Length, 0, Direction.N), rover.CurrentPosition);
            rover.TurnRight();
            rover.MoveForward();
            Assert.AreEqual((0, 0, Direction.E), rover.CurrentPosition);
        }

        [TestMethod]
        public void CollisionDetect()
        {
            var rover = new Rover(surface!);
            var start = (49, 50, Direction.W);
            rover.CurrentPosition = start;
            Assert.IsFalse(rover.MoveBackward());
            Assert.AreEqual(start, rover.CurrentPosition);
        }

    }

    [TestClass]
    public class TestCommands
    {
        ISurface? surface;
        [TestInitialize]
        public void Init()
        {
            surface = new Planet(100, 100);
            surface.AddObstacle(50, 50);
        }

        [TestMethod]
        public void StringCommands()
        {
            Rover rover = new Rover(surface!);
            var command = new CommandCenter();
            command.InitRoverPosition(0, 0, Direction.N, rover);
            command.SendCommands("fflbb", rover);
            Assert.AreEqual((2, 2, Direction.W), rover.CurrentPosition);
        }

        [TestMethod]
        public void StringCommands2()
        {
            Rover rover = new Rover(surface!);
            var command = new CommandCenter();
            command.InitRoverPosition(1, 1, Direction.N, rover);
            command.SendCommands("fflff", rover);
            Assert.AreEqual((surface!.Length, 3, Direction.W), rover.CurrentPosition);
        }

        [TestMethod]
        public void StringCommands3()
        {
            Rover rover = new Rover(surface!);
            var command = new CommandCenter();
            command.InitRoverPosition(0, 0, Direction.S, rover);
            command.SendCommands("frfrfrfr", rover);
            Assert.AreEqual((0, 0, Direction.S), rover.CurrentPosition);
            command.SendCommands("flflflfl", rover);
            Assert.AreEqual((0, 0, Direction.S), rover.CurrentPosition);
        }

        [TestMethod]
        public void StringCommands4()
        {
            Rover rover = new Rover(surface!);
            var command = new CommandCenter();
            command.InitRoverPosition(0, 0, Direction.N, rover);
            command.SendCommands("ffrfflffrff", rover);
            Assert.AreEqual((4, 4, Direction.E), rover.CurrentPosition);
        }

        [TestMethod]
        public void FailingStringCommands()
        {
            Rover rover = new Rover(surface!);
            var command = new CommandCenter();
            
            rover.CurrentPosition = (48, 50, Direction.E);
            Assert.IsFalse(command.SendCommands("fflbb",rover));
            Assert.AreEqual((49, 50, Direction.E), rover.CurrentPosition);
        }
    }

    [TestClass]
    public class TestAPI
    {
        ISurface? surface;
        [TestInitialize]
        public void Init()
        {
            surface = new Planet(100, 100);
            surface.AddObstacle(50, 50);
        }

        [TestMethod]
        public void ApiSendCommands()
        {
            IApiInterface api = new RoverApiInterface();
            api.Init(surface!);
            api.SetInitialRoverPosition(1, 1, Direction.N);
            api.SendCommands("fflff", out bool obstacle);
            Assert.IsFalse(obstacle);
            Assert.AreEqual((surface!.Length, 3, Direction.W), api.CurrentRoverPosition);
        }

        [TestMethod]
        public void ApiSendCommands2()
        {
            IApiInterface api = new RoverApiInterface();
            api.Init(surface!);
            api.SetInitialRoverPosition(0, 0, Direction.N);
            api.SendCommands("ffrfflffrff", out bool obstacle);
            Assert.IsFalse(obstacle);
            Assert.AreEqual((4, 4, Direction.E), api.CurrentRoverPosition);
        }

        [TestMethod]
        public void FailingStringCommands()
        {
            IApiInterface api = new RoverApiInterface();
            api.Init(surface!);
            api.SetInitialRoverPosition(48, 50, Direction.E);
            Assert.IsFalse(api.SendCommands("fflff", out bool obstacle));
            Assert.IsTrue(obstacle);
            Assert.AreEqual((49, 50, Direction.E), api.CurrentRoverPosition);
            Assert.AreEqual((50, 50), api.LastDetectedObstacle);
        }

        [TestMethod]
        public void WrongStringCommands()
        {
            IApiInterface api = new RoverApiInterface();
            api.Init(surface!);
            api.SetInitialRoverPosition(48, 50, Direction.E);
            Assert.IsTrue(api.SendCommands("asdf", out bool obstacle));
            Assert.IsFalse(obstacle);
            Assert.IsTrue(api.SendCommands("asdkj", out obstacle));
            Assert.IsFalse(obstacle);
            Assert.AreEqual((49, 50, Direction.E), api.CurrentRoverPosition);
        }
    }
}
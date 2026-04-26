using System;
using System.Collections.Generic;
using GlslWallpaper.OpenGL;


namespace GlslWallpaper
{
    /// <summary>
    /// OpenGL shader program and its uniform variable list set class.
    /// </summary>
    public class ProgramSet : IDisposable
    {
        /// <summary>
        /// OpenGL program.
        /// </summary>
        public GLProgram Program { get; }
        /// <summary>
        /// The list of Attribute locations of vertex position.
        /// </summary>
        public List<int> AttributePositionList { get; } = [];
        /// <summary>
        /// The list of uniform locations of window resolution.
        /// </summary>
        public List<int> UniformResolutionList { get; } = [];
        /// <summary>
        /// The list of uniform locations of window resolution (3D-Vector).
        /// </summary>
        public List<int> UniformResolution3List { get; } = [];
        /// <summary>
        /// The list of uniform locations of elapsed time in seconds.
        /// </summary>
        public List<int> UniformTimeList { get; } = [];
        /// <summary>
        /// The list of uniform locations of elapsed time in seconds since the previous frame.
        /// </summary>
        public List<int> UniformTimeDeltaList { get; } = [];
        /// <summary>
        /// The list of uniform locations of framerate.
        /// </summary>
        public List<int> UniformFrameRateList { get; } = [];
        /// <summary>
        /// The list of uniform locations of frame count.
        /// </summary>
        public List<int> UniformFrameCountList { get; } = [];
        /// <summary>
        /// The list of uniform locations of unnormalized mouse position.
        /// </summary>
        public List<int> UniformMouseList { get; } = [];
        /// <summary>
        /// The list of uniform locations of unnormalized mouse position (xy: current position, zw: click position).
        /// </summary>
        public List<int> UniformMouse4List { get; } = [];
        /// <summary>
        /// The list of uniform locations of current data.
        /// (x: year, y: month, z: day, w: time in seconds).
        /// </summary>
        public List<int> UniformDateList { get; } = [];
        /// <summary>
        /// A flag property which indicates this instance is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }


        /// <summary>
        /// Create instance with specified <see cref="GLProgram"/>.
        /// </summary>
        /// <param name="program">OpenGL program.</param>
        public ProgramSet(GLProgram program)
        {
            Program = program;
        }



        /// <summary>
        /// Use this program.
        /// </summary>
        public void Use()
        {
            GL.UseProgram(Program);
        }

        /// <summary>
        /// Set window resolution to the related uniform variables.
        /// </summary>
        /// <param name="width">Width of resolution.</param>
        /// <param name="height">Height of resolution.</param>
        public void SetUniformResolution(float width, float height)
        {
            foreach (var uResolution in UniformResolutionList)
            {
                GL.Uniform(uResolution, width, height);
            }
            foreach (var uResolution3 in UniformResolution3List)
            {
                GL.Uniform(uResolution3, width, height, 0.0f);
            }
        }

        /// <summary>
        /// Set elapsed time to the related uniform variables.
        /// </summary>
        /// <param name="time">elapsed time in seconds.</param>
        public void SetUniformTime(float time)
        {
            foreach (var uTime in UniformTimeList)
            {
                GL.Uniform(uTime, time);
            }
        }

        /// <summary>
        /// Set delta time to the related uniform variables.
        /// </summary>
        /// <param name="timeDelta">Elapsed time in seconds since the previous frame.</param>
        public void SetUniformTimeDelta(float timeDelta)
        {
            foreach (var uTimeDelta in UniformTimeDeltaList)
            {
                GL.Uniform(uTimeDelta, timeDelta);
            }
        }

        /// <summary>
        /// Set framerate to the related uniform variables.
        /// </summary>
        /// <param name="frameRate">Framerate.</param>
        public void SetUniformFrameRate(float frameRate)
        {
            foreach (var uFrameRate in UniformFrameRateList)
            {
                GL.Uniform(uFrameRate, frameRate);
            }
        }

        /// <summary>
        /// Set frame count to the related uniform variables.
        /// </summary>
        /// <param name="frameCount">Framerate.</param>
        public void SetUniformFrameCount(int frameCount)
        {
            foreach (var uFrameCount in UniformFrameCountList)
            {
                GL.Uniform(uFrameCount, frameCount);
            }
        }

        /// <summary>
        /// Set mouse position to the related uniform variables.
        /// </summary>
        /// <param name="x">X-coordinate of mouse position.</param>
        /// <param name="y">Y-coordinate of mouse position.</param>
        public void SetUniformMouse(float x, float y)
        {
            foreach (var uMouse in UniformMouseList)
            {
                GL.Uniform(uMouse, x, y);
            }
            foreach (var uMouse4 in UniformMouse4List)
            {
                GL.Uniform(uMouse4, x, y, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Set date time to the related uniform variables.
        /// </summary>
        /// <param name="dt">Date time.</param>
        public void SetUniformDate(DateTime dt)
        {
            foreach (var uDate in UniformDateList)
            {
                GL.Uniform(uDate, (float)dt.Year, (float)dt.Month, (float)dt.Day, (float)(dt.Hour * 3600 + dt.Minute * 60 + dt.Second));
            }
        }

        /// <summary>
        /// Get attribute location of the specified name of the attribute variable for vertex position, and add to <see cref="AttributePositionList"/>.
        /// </summary>
        /// <param name="name">Attribute variable name of the vertex position.</param>
        public void AddAttributePosition(string name)
        {
            AttributePositionList.Add(GL.GetAttribLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for window resolution, and add to <see cref="UniformResolutionList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the window resolution.</param>
        public void AddUniformResolution(string name)
        {
            UniformResolutionList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// <para>Get uniform location of the specified name of the uniform variable for window resolution, and add to <see cref="UniformResolution3List"/>.</para>
        /// <para>This uniform variable is float3, and its z component is fixed at 0.</para>
        /// </summary>
        /// <param name="name">Uniform variable name of the window resolution.</param>
        public void AddUniformResolution3(string name)
        {
            UniformResolution3List.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the elapsed time in seconds, and add to <see cref="UniformTimeList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the elapsed time in seconds.</param>
        public void AddUniformTime(string name)
        {
            UniformTimeList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the elapsed time in seconds since the previous frame, and add to <see cref="UniformTimeDeltaList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the elapsed time in seconds since the previous frame.</param>
        public void AddUniformTimeDelta(string name)
        {
            UniformTimeDeltaList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the framerate, and add to <see cref="UniformFrameRateList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the framerate.</param>
        public void AddUniformFrameRate(string name)
        {
            UniformFrameRateList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the frame count, and add to <see cref="UniformFrameCountList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the frame count.</param>
        public void AddUniformFrameCount(string name)
        {
            UniformFrameCountList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the current mouse position, and add to <see cref="UniformMouseList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the current mouse position.</param>
        public void AddUniformMouse(string name)
        {
            UniformMouseList.Add(GL.GetUniformLocation(Program, name));
        }
        /// <summary>
        /// <para>Get uniform location of the specified name of the uniform variable for the current mouse position and last click position, and add to <see cref="UniformMouse4List"/>.</para>
        /// <para>Since click position is not implemented, it is fixed at 0.</para>
        /// </summary>
        /// <param name="name">Uniform variable name of the current mouse position and last click position.</param>
        public void AddUniformMouse4(string name)
        {
            UniformMouse4List.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Get uniform location of the specified name of the uniform variable for the current mouse position, and add to <see cref="UniformMouseList"/>.
        /// </summary>
        /// <param name="name">Uniform variable name of the current mouse position.</param>
        public void AddUniformDate(string name)
        {
            UniformDateList.Add(GL.GetUniformLocation(Program, name));
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Release resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
        /// <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                Program.Dispose();
            }

            IsDisposed = true;
        }
    }
}

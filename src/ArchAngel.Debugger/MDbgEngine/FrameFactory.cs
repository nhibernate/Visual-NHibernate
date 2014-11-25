//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Samples.Debugging.CorDebug;


namespace Microsoft.Samples.Debugging.MdbgEngine
{
	// Abstract interfaces that allow plugable implementation of stack-walking into mdbg.

	/// <summary>
	/// Interface used for creating a new stackwalkers after a process is synchronized.
	/// </summary>
	public interface IMDbgFrameFactory
	{
		/// <summary>
		/// Creates a new StackWalker.
		/// </summary>
		/// <param name="thread">a thread object associated with the stackwalker</param>
		/// <returns>object implementing MDbgStackWalker interface</returns>
		IMDbgStackWalker CreateStackWalker(MDbgThread thread);

		/// <summary>
		/// Invalidates all stackwalkers created by this factory.
		/// </summary>
		/// <remarks>
		/// After the function is called, IsValid property on the stack walker object
		/// will return false and any other method call will throw an exception. 
		/// </remarks>
		void InvalidateStackWalkers();
	}

	/// <summary>
	/// Generic Stack Walker interface for traversing stacks in mdbg.
	/// </summary>
	public interface IMDbgStackWalker
	{
		/// <summary>
		/// Returns true if the object is valid and its methods can be called.
		/// </summary>
		bool IsUsable { get; }

		/// <summary>
		/// Function returns mdbg frame for the thred it was created for. 
		/// </summary>
		/// <param name="index">index of the frame. The leaf-most frame is indexed with 0.</param>
		/// <returns>the object representing frame</returns>
		/// <remarks>
		/// When the stack has 10 frames, it returns the frame for indexes 0-9. It returns null
		/// for index 10 and throws otherwise.
		/// </remarks>
		MDbgFrame GetFrame(int index);

		/// <summary>
		/// The function returns the index of the frame in the stack.
		/// </summary>
		/// <param name="frame">A frame returned with call to GetFrame.</param>
		/// <returns>an index of the frame</returns>
		/// <remarks>
		/// If the frame passed in was not created with this StackWalker object the function
		/// throws an exception.
		/// </remarks>
		int GetFrameIndex(MDbgFrame frame);
	}

	// Shared base classes among V2 & V3 implementations

	/// <summary>
	/// A base class for a Frame Factory implementations 
	/// </summary>
	public abstract class MDbgFrameFactoryBase : IMDbgFrameFactory
	{
		/// <summary>
		/// Creates a new StackWalker.
		/// </summary>
		/// <param name="thread">a thread object associated with the stackwalker</param>
		/// <returns>object implementing MDbgStackWalker interface</returns>
		public abstract IMDbgStackWalker CreateStackWalker(MDbgThread thread);

		/// <summary>
		/// Invalidates all stackwalkers created by this factory.
		/// </summary>
		/// <remarks>
		/// After the function is called, IsValid property on the stack walker object
		/// will return false and any other method call will throw an exception. 
		/// </remarks>
		public void InvalidateStackWalkers()
		{
			++m_logicalStopClock;
		}

		internal int m_logicalStopClock = -1;
	}

	/// <summary>
	/// A base class for a Stack Walker Implementations
	/// </summary>
	public abstract class MDbgStackWalkerBase : IMDbgStackWalker
	{
		/// <summary>
		/// Creates a stack walker and associates the walker with the frame factory and a thread.
		/// </summary>
		/// <param name="factory">FrameFactory that created the stackwalker</param>
		/// <param name="thread">Thread associated the the stack walker</param>
		protected MDbgStackWalkerBase(MDbgFrameFactoryBase factory, MDbgThread thread)
		{
			m_thread = thread;
			m_factory = factory;
			m_logicalStopClock = factory.m_logicalStopClock;
		}

		/// <summary>
		/// Returns true if the object is still valid.
		/// </summary>
		public bool IsUsable
		{
			get
			{
				Debug.Assert(m_logicalStopClock <= m_factory.m_logicalStopClock, "Cannot have counter from future");
				return m_logicalStopClock == m_factory.m_logicalStopClock;
			}
		}

		/// <summary>
		/// Function returns mdbg frame for the thred it was created for. 
		/// </summary>
		/// <param name="index">index of the frame. The leaf-most frame is indexed with 0.</param>
		/// <returns>the object representing frame</returns>
		/// <remarks>
		/// When the stack has 10 frames, it returns the frame for indexes 0-9. It returns null
		/// for index 10 and throws otherwise.
		/// </remarks>
		public MDbgFrame GetFrame(int index)
		{
			CheckUsability();
			return GetFrameImpl(index);
		}

		/// <summary>
		/// The function returns the index of the frame in the stack.
		/// </summary>
		/// <param name="frame">A frame returned with call to GetFrame.</param>
		/// <returns>an index of the frame</returns>
		/// <remarks>
		/// If the frame passed in was not created with this StackWalker object the function
		/// throws an exception.
		/// </remarks>
		public int GetFrameIndex(MDbgFrame frame)
		{
			CheckUsability();

			if (m_frameCache != null)
			{
				for (int i = 0; i < m_frameCache.Count; ++i)
				{
					if (m_frameCache[i] == frame)
					{
						return i;
					}
				}
			}

			throw new ArgumentException("Invalid frame");
		}
		/// <summary>
		/// A function that returns the new MdbgFrame. The function is expected to be overriden by derived implementations.
		/// </summary>
		/// 
		protected abstract MDbgFrame GetFrameImpl(int index);

		/// <summary>
		/// The function throws if the FrameFactory invalidated the Stackwalker.
		/// </summary>
		protected void CheckUsability()
		{
			if (!this.IsUsable)
				throw new InvalidOperationException("Reading old stack frames");
		}

		/// <summary>
		/// Thread associated with the stack-walker
		/// </summary>
		protected MDbgThread Thread
		{
			get
			{
				return m_thread;
			}
		}

		/// <summary>
		/// cache of frames created by the stack walker
		/// </summary>
		protected IList<MDbgFrame> FrameCache
		{
			get
			{
				if (m_frameCache == null)
				{
					m_frameCache = new List<MDbgFrame>();
				}
				return m_frameCache;
			}
		}

		private MDbgThread m_thread;
		private List<MDbgFrame> m_frameCache;

		private int m_logicalStopClock;
		private MDbgFrameFactoryBase m_factory;
	}



	// V2 implementation of plugable stack-walking API

	/// <summary>
	/// Implementation of FrameFactory that creates a StackWalker that uses debugger V2 StackWalking API 
	/// </summary>
	public class MDbgV2FrameFactory : MDbgFrameFactoryBase
	{

		/// <summary>
		/// Creates a new V2 StackWalker.
		/// </summary>
		/// <param name="thread">a thread object associated with the stackwalker</param>
		/// <returns>object implementing MDbgStackWalker interface</returns>
		override public IMDbgStackWalker CreateStackWalker(MDbgThread thread)
		{
			return new MDbgV2StackWalker(this, thread);
		}
	}

	/// <summary>
	/// A stack walker that uses a V2 API for stack traversal
	/// </summary>
	public class MDbgV2StackWalker : MDbgStackWalkerBase
	{
		/// <summary>
		/// Creates a new stack walker object.
		/// </summary>
		/// <param name="factory">FrameFactory owning this walker</param>
		/// <param name="thread">Thread associated with the stackwalker</param>
		public MDbgV2StackWalker(MDbgFrameFactoryBase factory, MDbgThread thread)
			: base(factory, thread)
		{
		}

		/// <summary>
		/// A function that returns the new MdbgFrame. The function is expected to be overriden by derived implementations.
		/// </summary>
		/// <param name="index">0 based index from top of the stack</param>
		/// <returns>frame from the stack</returns>
		protected override MDbgFrame GetFrameImpl(int index)
		{
			if (index < FrameCache.Count)
			{
				return FrameCache[index];
			}

			MDbgFrame frameToReturn;
			if (index == 0)
			{
				// special case the first frame
				frameToReturn = ReturnLeafFrame();
			}
			else
			{
				// use recursion...
				MDbgFrame prevFrame = GetFrameImpl(index - 1);
				if (prevFrame == null)
					throw new ArgumentException();

				frameToReturn = GetFrameCaller(prevFrame);
				if (frameToReturn == null)
				{
					// we need to get the next frame from the following chain

					CorChain chain = GetFrameChain(prevFrame);
					Debug.Assert(chain != null);
					// 1. find next chain
					while (true)
					{
						chain = chain.Caller;
						if (chain == null)
						{
							break;
						}

						if (chain.IsManaged)
						{
							CorFrame f = chain.ActiveFrame;
							if (f != null)
							{
								frameToReturn = new MDbgILFrame(Thread, f);
								break;
							}
						}
						else
						{
							frameToReturn = FillAndGetLeafFrameFromNativeChain(chain);
							if (frameToReturn != null)
							{
								break;
							}
						}
					}
				}
			}

			// store and return frameToReturn
			if (frameToReturn != null)
			{
				Debug.Assert(FrameCache.Count >= index);
				if (FrameCache.Count == index)
				{
					FrameCache.Add(frameToReturn);
				}
				else
				{
					Debug.Assert(FrameCache[index] == frameToReturn, "List of frames pre-filled with incorrect frame");
				}
			}
			return frameToReturn;
		}

		/// <summary>
		/// Finds the leaf-most frame in the stack.
		/// </summary>
		/// <returns>the top-most frame in the stack</returns>
		protected MDbgFrame ReturnLeafFrame()
		{
			MDbgFrame leafFrame = null;

			CorChain c = null;
			try
			{
				c = Thread.CorThread.ActiveChain;
			}
			catch (System.Runtime.InteropServices.COMException ce)
			{
				// Sometimes we cannot get the callstack.  For example, the thread
				// may not be scheduled yet (CORDBG_E_THREAD_NOT_SCHEDULED),
				// or the CLR may be corrupt (CORDBG_E_BAD_THREAD_STATE).
				// In either case, we'll ignore the problem and return an empty callstack.
				Debug.Assert(ce.ErrorCode == (int)HResult.CORDBG_E_BAD_THREAD_STATE ||
					ce.ErrorCode == (int)HResult.CORDBG_E_THREAD_NOT_SCHEDULED);
			}

			if (c != null)
			{
				if (!c.IsManaged)
				{
					leafFrame = FillAndGetLeafFrameFromNativeChain(c);
				}

				if (leafFrame == null)
				{
					// if we still have no frame, we'll get one from the managed code.
					while (c != null &&
						  (!c.IsManaged || (c.IsManaged && c.ActiveFrame == null))
						  )
					{
						c = c.Caller;
					}

					if (c == null)
					{
						leafFrame = null;
					}
					else
					{
						Debug.Assert(c != null && c.IsManaged);
						leafFrame = new MDbgILFrame(Thread, c.ActiveFrame);
					}
				}
			}
			else
			{
				leafFrame = null;
			}

			return leafFrame;
		}

		/// <summary>
		/// The function should parse the native part of the stack and fill the m_frameCache with the 
		/// corresponding frames from the native chain. Further it should return the top-most frame in the chain.
		/// </summary>
		/// <param name="chain">the native chain on the stack</param>
		/// <returns>First frame from the native chain or null if there are no frames</returns>
		protected virtual MDbgFrame FillAndGetLeafFrameFromNativeChain(CorChain chain)
		{
			Debug.Assert(chain != null);
			Debug.Assert(!chain.IsManaged);

			// StackWalkers that support interop callstacks would want to fill the list of frames and return 
			// topmost native frame from that chain.
			return null;
		}

		/// <summary>
		/// Returns the caller of the frame.
		/// </summary>
		/// <param name="frame">frame to return the caller of</param>
		/// <returns>the caller of the frame or null if the frame is the last frame in the chain</returns>
		protected virtual MDbgFrame GetFrameCaller(MDbgFrame frame)
		{
			Debug.Assert(frame != null);

			CorFrame f = frame.CorFrame.Caller;
			if (f == null)
			{
				return null;
			}

			return new MDbgILFrame(Thread, f);
		}

		/// <summary>
		/// Return the chain the frame belongs to.
		/// </summary>
		/// <param name="frame">frame created by this stackwalker</param>
		/// <returns>The chain that owns the frame.</returns>
		protected virtual CorChain GetFrameChain(MDbgFrame frame)
		{
			Debug.Assert(frame != null);
			return frame.CorFrame.Chain;
		}
	}
}

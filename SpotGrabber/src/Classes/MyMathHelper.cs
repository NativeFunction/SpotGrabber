using System;
using Microsoft.Xna.Framework;

namespace SpotGrabber
{
	public static class MyMathHelper
	{

		public static float DegToRad(float degrees)
        {
			return degrees * ((float)Math.PI / 180.0f);
		}

		public static float RadToDeg(float radians)
        {
			return radians * (180.0f / (float)Math.PI);
		}

		public static Vector3 GetNewVelocity(Vector3 from, Vector3 to, Vector3 speed)
		{
			Vector3 distance = to - from;
			distance.Normalize();
			return distance * speed;
		}

		public static int Clamp(int value, int min, int max)
		{
			if (value < min) value = min;
			else if (value > max) value = max;
			return value;
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value < min) value = min;
			else if (value > max) value = max;
			return value;
		}

		public static bool IsInsideThreshold(float value, float target, float threshhold)
		{
			return value >= target - threshhold && value <= target + threshhold;
		}

		/// <summary>
		/// Calculates the angle betweem two vectors
		/// </summary>
		public static float GetAngle(Vector2 v1, Vector2 v2)
		{
			float x = v2.X - v1.X;
			float y = v2.Y - v1.Y;
			float result = (float)Math.Atan2(y, x);
			return WrapAngle(result);// - MathHelper.Pi);
		}

		//public static float GetAngle(Vector2 one, Vector2 two)
		//{
		//    float xDiff = (float)(one.X - two.X);
		//    float yDiff = (float)(one.Y - two.Y);
		//    if (xDiff == 0)
		//    {
		//        xDiff = 1;//prevent DivideByZeroException
		//    }
		//    float baselineRotation = (float)Math.Atan(Math.Abs(yDiff / xDiff));
		//    baselineRotation = -baselineRotation;
		//    baselineRotation += 1.57f;
		//    if (xDiff < 0 && yDiff < 0)
		//    {
		//        baselineRotation = (float)Math.PI - baselineRotation;
		//    }
		//    else if (xDiff > 0 && yDiff < 0)
		//    {
		//        baselineRotation = (float)Math.PI + baselineRotation;
		//    }
		//    else if (xDiff > 0 && yDiff > 0)
		//    {
		//        baselineRotation = (float)Math.PI * 2 - baselineRotation;
		//    }
		//    return baselineRotation;
		//}

		/// Find the angle between two vectors. This will not only give the angle difference, but the direction.  
		/// For example, it may give you -1 radian, or 1 radian, depending on the direction. Angle given will be the   
		/// angle from the FromVector to the DestVector, in radians.  
		/// </summary>  
		/// <param name="FromVector">Vector to start at.</param>  
		/// <param name="DestVector">Destination vector.</param>  
		/// <param name="DestVectorsRight">Right vector of the destination vector</param>  
		/// <returns>Signed angle, in radians</returns>          
		/// <remarks>All three vectors must lie along the same plane.</remarks>  
		public static double GetSignedAngleBetween2DVectors(Vector3 fromVector, Vector3 destVector)
		{
			Vector3 right = destVector - fromVector;
			right.Normalize();
			right = Vector3.Cross(right, Vector3.Right);
			return GetSignedAngleBetween2DVectors(fromVector, destVector, right);
		}

		public static double GetSignedAngleBetween2DVectors(Vector3 fromVector, Vector3 destVector, Vector3 destVectorsRight)
		{
			fromVector.Normalize();
			destVector.Normalize();
			destVectorsRight.Normalize();

			float forwardDot = Vector3.Dot(fromVector, destVector);
			float rightDot = Vector3.Dot(fromVector, destVectorsRight);

			// Keep dot in range to prevent rounding errors  
			forwardDot = MathHelper.Clamp(forwardDot, -1.0f, 1.0f);

			double angleBetween = Math.Acos((float)forwardDot);

			if (rightDot < 0.0f)
				angleBetween = -angleBetween;

			if (destVector.X > 0f && angleBetween > 0f)
				angleBetween = -angleBetween;

			return angleBetween;
		}

		public static double GetSignedAngleBetween2DVectors(Vector2 fromVector, Vector2 destVector)
		{
			return GetSignedAngleBetween2DVectors(Convert3D(fromVector), Convert3D(destVector));
		}

		public static float TurnToFace(Vector2 fromVector, Vector2 destVector, float currentAngle, float turnSpeed)
		{
			float newRotation = (float)GetSignedAngleBetween2DVectors(Convert3D(fromVector), Convert3D(destVector));
			float difference = MathHelper.Clamp(newRotation - currentAngle, -turnSpeed, turnSpeed);
			bool reverse = Math.Abs(newRotation - currentAngle) > MathHelper.Pi;
			return WrapAngle(reverse ? currentAngle - difference : currentAngle + difference);
		}

		public static float TurnToFace(float currentAngle, float targetAngle, float turnSpeed)
		{
			float difference = MathHelper.Clamp(targetAngle - currentAngle, -turnSpeed, turnSpeed);
			bool reverse = Math.Abs(targetAngle - currentAngle) > MathHelper.Pi;
			return WrapAngle(reverse ? currentAngle - difference : currentAngle + difference);
		}

		public static Vector2 Convert2D(Vector3 v)
		{
			return new Vector2(v.X, v.Z);
		}

		public static Vector3 Convert3D(Vector2 v)
		{
			return new Vector3(v.X, 0f, v.Y);
		}

		/// <summary>
		/// Calculates the angle that an object should face, given its position, its
		/// target's position, its current angle, and its maximum turning speed.
		/// </summary>
		public static float TurnToFace2(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
		{
			// consider this diagram:
			//         C 
			//        /|
			//      /  |
			//    /    | y
			//  / o    |
			// S--------
			//     x
			// 
			// where S is the position of the spot light, C is the position of the cat,
			// and "o" is the angle that the spot light should be facing in order to 
			// point at the cat. we need to know what o is. using trig, we know that
			//      tan(theta)       = opposite / adjacent
			//      tan(o)           = y / x
			// if we take the arctan of both sides of this equation...
			//      arctan( tan(o) ) = arctan( y / x )
			//      o                = arctan( y / x )
			// so, we can use x and y to find o, our "desiredAngle."
			// x and y are just the differences in position between the two objects.
			float x = faceThis.X - position.X;
			float y = faceThis.Y - position.Y;

			// we'll use the Atan2 function. Atan will calculates the arc tangent of 
			// y / x for us, and has the added benefit that it will use the signs of x
			// and y to determine what cartesian quadrant to put the result in.
			// http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
			float desiredAngle = (float)Math.Atan2(y, x);

			// so now we know where we WANT to be facing, and where we ARE facing...
			// if we weren't constrained by turnSpeed, this would be easy: we'd just 
			// return desiredAngle.
			// instead, we have to calculate how much we WANT to turn, and then make
			// sure that's not more than turnSpeed.

			// first, figure out how much we want to turn, using WrapAngle to get our
			// result from -Pi to Pi ( -180 degrees to 180 degrees )
			float difference = WrapAngle(desiredAngle - currentAngle);

			// clamp that between -turnSpeed and turnSpeed.
			difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);

			// so, the closest we can get to our target is currentAngle + difference.
			// return that, using WrapAngle again.
			return WrapAngle(currentAngle + difference);
		}

		/// <summary>
		/// Returns the angle expressed in radians between -Pi and Pi.
		/// </summary>
		public static float WrapAngle(float radians)
		{
			while (radians < -MathHelper.Pi)
			{
				radians += MathHelper.TwoPi;
			}
			while (radians > MathHelper.Pi)
			{
				radians -= MathHelper.TwoPi;
			}
			return radians;
		}

		//public static Vector3 RotateVectorByAngle2(Vector3 origin, Vector3 dir, float angle)
		//{
		//    float currentAngle = (float)GetSignedAngleBetween2DVectors(Vector3.Forward, dir);

		//    Matrix matrix = Matrix.Identity *
		//        Matrix.CreateTranslation(Vector3.Forward) *
		//        Matrix.CreateRotationY(angle + currentAngle);

		//    Vector3 result = Vector3.Transform(Vector3.Zero, matrix);
		//    return result * (dir - origin);
		//}

		//public static Vector3 RotateVectorByAngle(Vector3 origin, Vector3 dir, float angle)
		//{
		//    Matrix matrix = Matrix.Identity *
		//        Matrix.CreateTranslation(dir) *
		//        Matrix.CreateRotationY(angle);

		//    return Vector3.Transform(origin, matrix);
		//}

		public static Vector2 RotateVector2ByAngle(Vector2 v, float angle)
		{
			var v3 = Vector3.Transform(new Vector3(v.X, 0, v.Y), Matrix.CreateRotationY(angle));
			return new Vector2(v3.X, v3.Z);
		}

		public static Vector3 RotateVector3ByAngle(Vector3 v, float angle)
		{
			return Vector3.Transform(v, Matrix.CreateRotationY(angle));
		}

		static Random rng = new Random();

		public static float Interpolate(float alpha, float x0, float x1)
		{
			return x0 + ((x1 - x0) * alpha);
		}

		public static Vector3 Interpolate(float alpha, Vector3 x0, Vector3 x1)
		{
			return x0 + ((x1 - x0) * alpha);
		}

		// ----------------------------------------------------------------------------
		// Random number utilities

		// Returns a float randomly distributed between 0 and 1
		public static float Random()
		{
			return (float)rng.NextDouble();
		}

		// Returns a float randomly distributed between lowerBound and upperBound
		public static float Random(float lowerBound, float upperBound)
		{
			return lowerBound + (Random() * (upperBound - lowerBound));
		}

		/// <summary>
		/// Constrain a given value (x) to be between two (ordered) bounds min
		/// and max.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns>Returns x if it is between the bounds, otherwise returns the nearer bound.</returns>
		public static float Clip(float x, float min, float max)
		{
			if (x < min) return min;
			if (x > max) return max;
			return x;
		}

		// ----------------------------------------------------------------------------
		// remap a value specified relative to a pair of bounding values
		// to the corresponding value relative to another pair of bounds.
		// Inspired by (dyna:remap-interval y y0 y1 z0 z1)
		public static float RemapInterval(float x, float in0, float in1, float out0, float out1)
		{
			// uninterpolate: what is x relative to the interval in0:in1?
			float relative = (x - in0) / (in1 - in0);

			// now interpolate between output interval based on relative x
			return Interpolate(relative, out0, out1);
		}

		// Like remapInterval but the result is clipped to remain between
		// out0 and out1
		public static float RemapIntervalClip(float x, float in0, float in1, float out0, float out1)
		{
			// uninterpolate: what is x relative to the interval in0:in1?
			float relative = (x - in0) / (in1 - in0);

			// now interpolate between output interval based on relative x
			return Interpolate(Clip(relative, 0, 1), out0, out1);
		}

		// ----------------------------------------------------------------------------
		// classify a value relative to the interval between two bounds:
		//     returns -1 when below the lower bound
		//     returns  0 when between the bounds (inside the interval)
		//     returns +1 when above the upper bound
		public static int IntervalComparison(float x, float lowerBound, float upperBound)
		{
			if (x < lowerBound) return -1;
			if (x > upperBound) return +1;
			return 0;
		}

		public static float ScalarRandomWalk(float initial, float walkSpeed, float min, float max)
		{
			float next = initial + (((Random() * 2) - 1) * walkSpeed);
			if (next < min) return min;
			if (next > max) return max;
			return next;
		}

		public static float Square(float x)
		{
			return x * x;
		}

		/// <summary>
		/// blends new values into an accumulator to produce a smoothed time series
		/// </summary>
		/// <remarks>
		/// Modifies its third argument, a reference to the float accumulator holding
		/// the "smoothed time series."
		/// 
		/// The first argument (smoothRate) is typically made proportional to "dt" the
		/// simulation time step.  If smoothRate is 0 the accumulator will not change,
		/// if smoothRate is 1 the accumulator will be set to the new value with no
		/// smoothing.  Useful values are "near zero".
		/// </remarks>
		/// <typeparam name="T"></typeparam>
		/// <param name="smoothRate"></param>
		/// <param name="newValue"></param>
		/// <param name="smoothedAccumulator"></param>
		/// <example>blendIntoAccumulator (dt * 0.4f, currentFPS, smoothedFPS)</example>
		public static void BlendIntoAccumulator(float smoothRate, float newValue, ref float smoothedAccumulator)
		{
			smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 1), smoothedAccumulator, newValue);
		}

		public static void BlendIntoAccumulator(float smoothRate, Vector3 newValue, ref Vector3 smoothedAccumulator)
		{
			smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 1), smoothedAccumulator, newValue);
		}

		public static Vector3 GetVectorFromAngle(int slice, int totalSlices)
		{
			float angle = slice * MathHelper.TwoPi / totalSlices;
			float dx = (float)Math.Cos(angle);
			float dz = (float)Math.Sin(angle);
			return new Vector3(dx, 0, dz);
		}

		public static Vector2 TransformToCircle(Vector2 position, float worldWidth)
		{
			float px = position.X - worldWidth * 0.25f;
			float step = MathHelper.TwoPi * (px / worldWidth);
			return new Vector2((float)Math.Cos(step) * position.Y, (float)Math.Sin(step) * position.Y);
		}
	}
}

public abstract class avector {
	public abstract double x{get; set;}
	public abstract double y{get; set;}	
	public abstract double z{get; set;}

	public avector add(avector other) {
		this.x = this.x + other.x;
		this.y = this.y + other.y;
		this.z = this.z + other.z;
		return this;
	}

	public avector sub(avector other) {
		this.x = this.x - other.x;
		this.y = this.y - other.y;
		this.z = this.z - other.z;
		return this;
	}

	public avector mult(double a) {
		this.x = this.x*a;
		this.y = this.y*a;
		this.z = this.z*a;
		return this;
	}	

	public double dot_product(avector other) {
		return this.x*other.x + this.y*other.y + this.z*other.z;
	} 

	public avector cross_product(avector other) {
		this.x = this.y*other.z - this.z*other.y;
		this.y = this.z*other.x - this.x*other.y;
		this.z = this.x*other.y - this.y*other.x;
		return this;
	}
	
	public double magnitude() {
		return System.Math.Sqrt(this.x*this.x + this.y*this.y + this.z*this.z);
	}

	public override string ToString() {
		return string.Format("[{0:f4}, {1:f4}, {2:f4}]", this.x, this.y, this.z);
	}

}

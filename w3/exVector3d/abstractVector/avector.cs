public abstract class avector {
	public abstract double x{get; set;}
	public abstract double y{get; set;}	
	public abstract double z{get; set;}

	public static avector operator+(avector v, avector u) {
		return _add(v, u);
	}

	public override string ToString() {
		return string.Format("[{0:f4}, {1:f4}, {2:f4}]", this.x, this.y, this.z);
	}

}

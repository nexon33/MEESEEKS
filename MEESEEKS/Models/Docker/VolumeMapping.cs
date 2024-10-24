namespace MEESEEKS.Models.Docker
{
    /// <summary>
    /// Represents a volume mapping between host and container.
    /// </summary>
    public class VolumeMapping
    {
        /// <summary>
        /// Path on the host machine.
        /// </summary>
        public string HostPath { get; set; }

        /// <summary>
        /// Path inside the container.
        /// </summary>
        public string ContainerPath { get; set; }

        /// <summary>
        /// Whether the volume is read-only.
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}

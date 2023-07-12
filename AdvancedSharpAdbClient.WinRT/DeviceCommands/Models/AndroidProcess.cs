// <copyright file="AndroidProcess.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Represents a process running on an Android device.
    /// </summary>
    public sealed class AndroidProcess
    {
        internal readonly AdvancedSharpAdbClient.DeviceCommands.AndroidProcess androidProcess;

        /// <summary>
        /// Gets or sets the state of the process.
        /// </summary>
        public AndroidProcessState State
        {
            get => (AndroidProcessState)androidProcess.State;
            set => androidProcess.State = (AdvancedSharpAdbClient.DeviceCommands.AndroidProcessState)value;
        }

        /// <summary>
        /// Gets or sets the name of the process, including arguments, if any.
        /// </summary>
        public string Name
        {
            get => androidProcess.Name;
            set => androidProcess.Name = value;
        }

        /// <summary>
        /// Gets or sets the parent Process ID number.
        /// </summary>
        public int ParentProcessId
        {
            get => androidProcess.ParentProcessId;
            set => androidProcess.ParentProcessId = value;
        }

        /// <summary>
        /// Gets or sets the Process Group ID number.
        /// </summary>
        public int ProcessGroupId
        {
            get => androidProcess.ProcessGroupId;
            set => androidProcess.ProcessGroupId = value;
        }

        /// <summary>
        /// Gets or sets the session ID of the process number.
        /// </summary>
        public int SessionID
        {
            get => androidProcess.SessionID;
            set => androidProcess.SessionID = value;
        }

        /// <summary>
        /// Gets or sets the Process ID number.
        /// </summary>
        public int ProcessId
        {
            get => androidProcess.ProcessId;
            set => androidProcess.ProcessId = value;
        }

        /// <summary>
        /// Gets or sets the controlling terminal of the process.
        /// </summary>
        /// <value>The minor device number is contained in the combination of bits
        /// <see langword="31"/> to <see langword="20"/> and <see langword="7"/> to <see langword="0"/>;
        /// the major device number is in bits <see langword="15"/> to <see langword="8"/>.</value>
        public int TTYNumber
        {
            get => androidProcess.TTYNumber;
            set => androidProcess.TTYNumber = value;
        }

        /// <summary>
        /// Gets or sets the ID of the foreground process group of the controlling terminal of the process.
        /// </summary>
        public int TopProcessGroupId
        {
            get => androidProcess.TopProcessGroupId;
            set => androidProcess.TopProcessGroupId = value;
        }

        /// <summary>
        /// Gets or sets The kernel flags word of the process. For bit meanings, see the <c>PF_*</c> defines
        /// in the Linux kernel source file <c>include/linux/sched.h</c>. Details depend on the kernel version.
        /// </summary>
        public PerProcessFlags Flags
        {
            get => (PerProcessFlags)androidProcess.Flags;
            set => androidProcess.Flags = (AdvancedSharpAdbClient.DeviceCommands.PerProcessFlags)value;
        }

        /// <summary>
        /// Gets or sets the number of minor faults the process has made
        /// which have not required loading a memory page from disk.
        /// </summary>
        public ulong MinorFaults
        {
            get => androidProcess.MinorFaults;
            set => androidProcess.MinorFaults = value;
        }

        /// <summary>
        /// Gets or sets the number of minor faults that the process's waited-for children have made.
        /// </summary>
        public ulong ChildMinorFaults
        {
            get => androidProcess.ChildMinorFaults;
            set => androidProcess.ChildMinorFaults = value;
        }

        /// <summary>
        /// Gets or sets the number of major faults the process has made
        /// which have required loading a memory page from disk.
        /// </summary>
        public ulong MajorFaults
        {
            get => androidProcess.MajorFaults;
            set => androidProcess.MajorFaults = value;
        }

        /// <summary>
        /// Gets or sets the number of major faults that the process's waited-for children have made.
        /// </summary>
        public ulong ChildMajorFaults
        {
            get => androidProcess.ChildMajorFaults;
            set => androidProcess.ChildMajorFaults = value;
        }

        /// <summary>
        /// Gets or sets the amount of time that this process has been scheduled in user mode,
        /// measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>). This includes guest time,
        /// guest_time (time spent running a virtual CPU, see below), so that applications
        /// that are not aware of the guest time field do not lose that time from their calculations.
        /// </summary>
        public ulong UserScheduledTime
        {
            get => androidProcess.UserScheduledTime;
            set => androidProcess.UserScheduledTime = value;
        }

        /// <summary>
        /// Gets or sets the amount of time that this process has been scheduled in kernel mode,
        /// measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>).
        /// </summary>
        public ulong ScheduledTime
        {
            get => androidProcess.ScheduledTime;
            set => androidProcess.ScheduledTime = value;
        }

        /// <summary>
        /// Gets or sets the amount of time that this process's waited-for children have been scheduled in user mode,
        /// measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>). (See also <c>times(2)</c>.)
        /// This includes guest time, cguest_time (time spent running a virtual CPU, see below).
        /// </summary>
        public long ChildUserScheduledTime
        {
            get => androidProcess.ChildUserScheduledTime;
            set => androidProcess.ChildUserScheduledTime = value;
        }

        /// <summary>
        /// Gets or sets the Amount of time that this process's waited-for children have been scheduled in kernel mode,
        /// measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>).
        /// </summary>
        public long ChildScheduledTime
        {
            get => androidProcess.ChildScheduledTime;
            set => androidProcess.ChildScheduledTime = value;
        }

        /// <summary>
        /// Gets or sets the value for processes running a real-time scheduling policy
        /// (policy below; see sched_setscheduler(2)), this is the negated scheduling priority,
        /// minus one; that is, a number in the range <see langword="-2"/> to <see langword="-100"/>,
        /// corresponding to real-time priorities <see langword="1"/> to <see langword="99"/>.
        /// For processes running under a non-real-time scheduling policy, this is the raw nice
        /// value (<c>setpriority(2)</c>) as represented in the kernel. The kernel stores nice values as numbers
        /// in the range <see langword="0"/> (high) to <see langword="39"/> (low),
        /// corresponding to the user-visible nice range of <see langword="-20"/> to <see langword="19"/>.
        /// </summary>
        public long Priority
        {
            get => androidProcess.Priority;
            set => androidProcess.Priority = value;
        }

        /// <summary>
        /// Gets or sets the nice value (see <c>setpriority(2)</c>), a value in the range
        /// <see langword="19"/> (low priority) to <see langword="-20"/> (high priority).
        /// </summary>
        public long Nice
        {
            get => androidProcess.Nice;
            set => androidProcess.Nice = value;
        }

        /// <summary>
        /// Gets or sets the number of threads in this process (since Linux 2.6).
        /// </summary>
        /// <remarks>Before kernel 2.6, this field was hard coded to 0 as a placeholder for an earlier removed field.</remarks>
        public long ThreadsNumber
        {
            get => androidProcess.ThreadsNumber;
            set => androidProcess.ThreadsNumber = value;
        }

        /// <summary>
        /// Gets or sets the time in jiffies before the next <c>SIGALRM</c> is sent to the process due to an interval timer.
        /// </summary>
        /// <remarks>Since kernel 2.6.17, this field is no longer maintained, and is hard coded as 0.</remarks>
        public long Interval
        {
            get => androidProcess.Interval;
            set => androidProcess.Interval = value;
        }

        /// <summary>
        /// Gets or sets The time the process started after system boot. In kernels before Linux 2.6,
        /// this value was expressed in jiffies.Since Linux 2.6, the value is expressed in clock ticks
        /// (divide by <c>sysconf(_SC_CLK_TCK)</c>).
        /// </summary>
        /// <remarks>The format for this field was %lu before Linux 2.6.</remarks>
        public ulong StartTime
        {
            get => androidProcess.StartTime;
            set => androidProcess.StartTime = value;
        }

        /// <summary>
        /// Gets or sets total virtual memory size in bytes.
        /// </summary>
        public ulong VirtualSize
        {
            get => androidProcess.VirtualSize;
            set => androidProcess.VirtualSize = value;
        }

        /// <summary>
        /// Gets or sets the process resident set size.
        /// </summary>
        /// <value>Number of pages the process has in real memory.
        /// This is just the pages which count toward text, data, or stack space.
        /// This does not include pages which have not been demand-loaded in, or which are swapped out.
        /// This value is inaccurate; see <c>/proc/[pid]/statm</c> below.</value>
        public int ResidentSetSize
        {
            get => androidProcess.ResidentSetSize;
            set => androidProcess.ResidentSetSize = value;
        }

        /// <summary>
        /// Gets or sets current soft limit in bytes on the rss of the process;
        /// See the description of RLIMIT_RSS in <c>getrlimit(2)</c>.
        /// </summary>
        public ulong ResidentSetSizeLimit
        {
            get => androidProcess.ResidentSetSizeLimit;
            set => androidProcess.ResidentSetSizeLimit = value;
        }

        /// <summary>
        /// Gets or sets the address above which program text can run.
        /// </summary>
        public ulong StartCode
        {
            get => androidProcess.StartCode;
            set => androidProcess.StartCode = value;
        }

        /// <summary>
        /// Gets or sets the address below which program text can run.
        /// </summary>
        public ulong EndCode
        {
            get => androidProcess.EndCode;
            set => androidProcess.EndCode = value;
        }

        /// <summary>
        /// Gets or sets the address of the start (i.e., bottom) of the stack.
        /// </summary>
        public ulong StartStack
        {
            get => androidProcess.StartStack;
            set => androidProcess.StartStack = value;
        }

        /// <summary>
        /// Gets or sets the current value of ESP (stack pointer), as found in the kernel stack page for the process.
        /// </summary>
        public ulong ESP
        {
            get => androidProcess.ESP;
            set => androidProcess.ESP = value;
        }

        /// <summary>
        /// Gets or sets the current EIP (instruction pointer).
        /// </summary>
        public ulong EIP
        {
            get => androidProcess.EIP;
            set => androidProcess.EIP = value;
        }

        /// <summary>
        /// Gets or sets the bitmap of pending signals, displayed as a decimal number. Obsolete,
        /// because it does not provide information on real-time signals; Use <c>/proc/[pid]/status</c> instead.
        /// </summary>
        public ulong Signal
        {
            get => androidProcess.Signal;
            set => androidProcess.Signal = value;
        }

        /// <summary>
        /// Gets or sets the bitmap of blocked signals, displayed as a decimal number. Obsolete,
        /// because it does not provide information on real-time signals; Use <c>/proc/[pid]/status</c> instead.
        /// </summary>
        public ulong Blocked
        {
            get => androidProcess.Blocked;
            set => androidProcess.Blocked = value;
        }

        /// <summary>
        /// Gets or sets the bitmap of ignored signals, displayed as a decimal number. Obsolete,
        /// because it does not provide information on real-time signals; Use <c>/proc/[pid]/status</c> instead.
        /// </summary>
        public ulong IgnoredSignals
        {
            get => androidProcess.IgnoredSignals;
            set => androidProcess.IgnoredSignals = value;
        }

        /// <summary>
        /// Gets or sets the bitmap of caught signals, displayed as a decimal number. Obsolete,
        /// because it does not provide information on real-time signals; Use <c>/proc/[pid]/status</c> instead.
        /// </summary>
        public ulong CaughtSignals
        {
            get => androidProcess.CaughtSignals;
            set => androidProcess.CaughtSignals = value;
        }

        /// <summary>
        /// Gets or sets the memory address of the event the process is waiting for.
        /// </summary>
        /// <value>This is the "channel" in which the process is waiting.
        /// It is the address of a location in the kernel where the process is sleeping.
        /// The corresponding symbolic name can be found in <c>/proc/[pid]/wchan</c>.</value>
        public ulong WChan
        {
            get => androidProcess.WChan;
            set => androidProcess.WChan = value;
        }

        /// <summary>
        /// Gets or sets the number of pages swapped (not maintained).
        /// </summary>
        public ulong SwappedPagesNumber
        {
            get => androidProcess.SwappedPagesNumber;
            set => androidProcess.SwappedPagesNumber = value;
        }

        /// <summary>
        /// Gets or sets the cumulative number of pages swapped for child processes (not maintained).
        /// </summary>
        public ulong CumulativeSwappedPagesNumber
        {
            get => androidProcess.CumulativeSwappedPagesNumber;
            set => androidProcess.CumulativeSwappedPagesNumber = value;
        }

        /// <summary>
        /// Gets or sets the signal to be sent to parent when we die.
        /// </summary>
        public int ExitSignal
        {
            get => androidProcess.ExitSignal;
            set => androidProcess.ExitSignal = value;
        }

        /// <summary>
        /// Gets or sets the CPU number last executed on.
        /// </summary>
        public int Processor
        {
            get => androidProcess.Processor;
            set => androidProcess.Processor = value;
        }

        /// <summary>
        /// Gets or sets the real-time scheduling priority, a number in the range 1 to 99 for processes scheduled
        /// under a real-time policy, or 0, for non-real-time processes (see <c>sched_setscheduler(2)</c>).
        /// </summary>
        public uint RealTimePriority
        {
            get => androidProcess.RealTimePriority;
            set => androidProcess.RealTimePriority = value;
        }

        /// <summary>
        /// Gets or sets the scheduling policy (see <c>sched_setscheduler(2)</c>).
        /// Decode using the <c>SCHED_*</c> constants in <c>linux/sched.h</c>.
        /// </summary>
        public uint Policy
        {
            get => androidProcess.Policy;
            set => androidProcess.Policy = value;
        }

        /// <summary>
        /// Gets or sets the aggregated block I/O delays, measured in clock ticks (centiseconds).
        /// </summary>
        public ulong IODelays
        {
            get => androidProcess.IODelays;
            set => androidProcess.IODelays = value;
        }

        /// <summary>
        /// Gets or sets the guest time of the process (time spent running a virtual CPU for a guest operating system),
        /// measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>).
        /// </summary>
        public ulong GuestTime
        {
            get => androidProcess.GuestTime;
            set => androidProcess.GuestTime = value;
        }

        /// <summary>
        /// Gets or sets the guest time of the process's children, measured in clock ticks (divide by <c>sysconf(_SC_CLK_TCK)</c>).
        /// </summary>
        public long ChildGuestTime
        {
            get => androidProcess.ChildGuestTime;
            set => androidProcess.ChildGuestTime = value;
        }

        /// <summary>
        /// Gets or sets the address above which program initialized and uninitialized(BSS) data are placed.
        /// </summary>
        public ulong StartData
        {
            get => androidProcess.StartData;
            set => androidProcess.StartData = value;
        }

        /// <summary>
        /// Gets or sets the address below which program initialized and uninitialized(BSS) data are placed.
        /// </summary>
        public ulong EndData
        {
            get => androidProcess.EndData;
            set => androidProcess.EndData = value;
        }

        /// <summary>
        /// Gets or sets the address above which program heap can be expanded with <c>brk(2)</c>.
        /// </summary>
        public ulong StartBrk
        {
            get => androidProcess.StartBrk;
            set => androidProcess.StartBrk = value;
        }

        /// <summary>
        /// Gets or sets the address above which program command-line arguments (<c>argv</c>) are placed.
        /// </summary>
        public ulong ArgStart
        {
            get => androidProcess.ArgStart;
            set => androidProcess.ArgStart = value;
        }

        /// <summary>
        /// Gets or sets the address below program command-line arguments (<c>argv</c>) are placed.
        /// </summary>
        public ulong ArgEnd
        {
            get => androidProcess.ArgEnd;
            set => androidProcess.ArgEnd = value;
        }

        /// <summary>
        /// Gets or sets the address above which program environment is placed.
        /// </summary>
        public ulong EnvStart
        {
            get => androidProcess.EnvStart;
            set => androidProcess.EnvStart = value;
        }

        /// <summary>
        /// Gets or sets the address below which program environment is placed.
        /// </summary>
        public ulong EnvEnd
        {
            get => androidProcess.EnvEnd;
            set => androidProcess.EnvEnd = value;
        }

        /// <summary>
        /// Gets or sets the thread's exit status in the form reported by <c>waitpid(2)</c>.
        /// </summary>
        public int ExitCode
        {
            get => androidProcess.ExitCode;
            set => androidProcess.ExitCode = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidProcess"/> class.
        /// </summary>
        public AndroidProcess() => androidProcess = new AdvancedSharpAdbClient.DeviceCommands.AndroidProcess();

        internal AndroidProcess(AdvancedSharpAdbClient.DeviceCommands.AndroidProcess androidProcess) => this.androidProcess = androidProcess;

        internal static AndroidProcess GetAndroidProcess(AdvancedSharpAdbClient.DeviceCommands.AndroidProcess androidProcess) => new(androidProcess);

        /// <summary>
        /// Creates a <see cref="AndroidProcess"/> from it <see cref="string"/> representation.
        /// </summary>
        /// <param name="line">A <see cref="string"/> which represents a <see cref="AndroidProcess"/>.</param>
        /// <param name="cmdLinePrefix">A value indicating whether the output of <c>/proc/{pid}/stat</c> is prefixed with <c>/proc/{pid}/cmdline</c> or not.
        /// Because <c>stat</c> does not contain the full process name, this can be useful.</param>
        /// <returns>The equivalent <see cref="AndroidProcess"/>.</returns>
        public static AndroidProcess Parse(string line, bool cmdLinePrefix) => GetAndroidProcess(AdvancedSharpAdbClient.DeviceCommands.AndroidProcess.Parse(line, cmdLinePrefix));

        /// <summary>
        /// Gets a <see cref="string"/> that represents this <see cref="AndroidProcess"/>,
        /// in the format of "<see cref="Name"/> (<see cref="ProcessId"/>)".
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="AndroidProcess"/>.</returns>
        public override string ToString() => androidProcess.ToString();
    }
}

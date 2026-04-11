-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 11, 2026 at 05:50 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `grading_system`
--
CREATE DATABASE IF NOT EXISTS `grading_system` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `grading_system`;

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `acc_id` int(11) NOT NULL,
  `email` varchar(250) DEFAULT NULL,
  `pword` varchar(255) DEFAULT NULL,
  `role` varchar(20) DEFAULT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `middlename` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `section` varchar(50) DEFAULT NULL,
  `gender` varchar(20) DEFAULT NULL,
  `course` varchar(100) DEFAULT NULL,
  `yr_lvl` int(11) DEFAULT NULL,
  `subject` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`acc_id`, `email`, `pword`, `role`, `firstname`, `middlename`, `lastname`, `section`, `gender`, `course`, `yr_lvl`, `subject`) VALUES
(1, 'admin', '827ccb0eea8a706c4c34a16891f84e7b', 'ADMIN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(8, 'Dar@gmail.com', '827ccb0eea8a706c4c34a16891f84e7b', 'teacher', 'Darie', 'Milano', 'Kafka', NULL, 'female', NULL, NULL, NULL),
(9, 'lei@gmail.com', '827ccb0eea8a706c4c34a16891f84e7b', 'student', 'lei', 'dumangas', 'librora', 'BSIT 1-1', 'male', 'BSIT', 1, NULL),
(10, 'lelei@gmail.com', '827ccb0eea8a706c4c34a16891f84e7b', 'student', 'lei andri', 'dumangas', 'librora', 'BSA 1-1', 'male', 'BSA', 1, NULL),
(11, 'kei@gmail.com', '827ccb0eea8a706c4c34a16891f84e7b', 'teacher', 'haliburton', 'james', 'heli', NULL, 'male', NULL, NULL, NULL),
(12, 'lebron@gmail.com', '827ccb0eea8a706c4c34a16891f84e7b', 'teacher', 'lebron', 'curry', 'james', NULL, 'male', NULL, NULL, NULL);

--
-- Triggers `account`
--
DELIMITER $$
CREATE TRIGGER `create_submission_row_for_new_teacher` AFTER INSERT ON `account` FOR EACH ROW BEGIN
  IF NEW.role = 'teacher' THEN
    INSERT IGNORE INTO grade_submission
      (prof_id, school_year, semester, submitted)
    SELECT p.prof_id, sc.school_year, sc.semester, 0
    FROM   prof p
    CROSS JOIN sem_control sc
    WHERE  p.acc_id = NEW.acc_id
    LIMIT 1;
  END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `insert_prof_if_role_prof` AFTER INSERT ON `account` FOR EACH ROW BEGIN
    IF NEW.role = 'teacher' THEN
        INSERT INTO prof
        (
            acc_id,
            firstname,
            middlename,
            lastname,
            email,
            gender,
            role
        )
        VALUES
        (
            NEW.acc_id,
            NEW.firstname,
            NEW.middlename,
            NEW.lastname,
            NEW.email,
            NEW.gender,
            NEW.role
        );
    END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `insert_student_if_role_student` AFTER INSERT ON `account` FOR EACH ROW BEGIN
    IF NEW.role = 'student' THEN
        INSERT INTO student
        (
            firstname,
            lastname,
            middlename,
            email,
            role,
            course,
            yr_lvl,
            gender,
            acc_id
            -- NOTE: section_id is intentionally omitted here
            -- It will be set by VB code after insert via UpdateStudentSectionId()
        )
        VALUES
        (
            NEW.firstname,
            NEW.lastname,
            NEW.middlename,
            NEW.email,
            NEW.role,
            NEW.course,
            NEW.yr_lvl,
            NEW.gender,
            NEW.acc_id
        );
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `course_id` int(11) NOT NULL,
  `course_code` varchar(20) NOT NULL,
  `course_name` varchar(100) NOT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`course_id`, `course_code`, `course_name`, `description`) VALUES
(1, 'BSIT', 'Bachelor of Science in Information Technology', 'IT and software development'),
(2, 'BSCS', 'Bachelor of Science in Computer Science', 'Computer science and theory'),
(3, 'BSA', 'Bachelor of Science in Accountancy', 'Accounting and finance'),
(4, 'BSN', 'Bachelor of Science in Nursing', 'Nursing and healthcare'),
(5, 'BSPSYCH', 'Bachelor of Science in Psychology', 'Psychology and behavioral science'),
(6, 'BSHRM', 'Bachelor of Science in Hotel and Restaurant Management', 'Hospitality management'),
(7, 'BSED', 'Bachelor of Secondary Education', 'Education and teaching'),
(8, 'BEED', 'Bachelor of Elementary Education', 'Elementary education'),
(9, 'BSECE', 'Bachelor of Science in Electronics and Communications Engineering', 'Electronics engineering'),
(10, 'BSCE', 'Bachelor of Science in Civil Engineering', 'Civil engineering'),
(11, 'BSME', 'Bachelor of Science in Mechanical Engineering', 'Mechanical engineering'),
(12, 'BSEE', 'Bachelor of Science in Electrical Engineering', 'Electrical engineering'),
(13, 'BSCpE', 'Bachelor of Science in Computer Engineering', 'Computer hardware and systems'),
(14, 'BSTM', 'Bachelor of Science in Tourism Management', 'Tourism and travel management'),
(15, 'BSBA', 'Bachelor of Science in Business Administration', 'Business and management');

-- --------------------------------------------------------

--
-- Table structure for table `grades`
--

CREATE TABLE `grades` (
  `grades_id` int(11) NOT NULL,
  `school_year` varchar(20) DEFAULT NULL,
  `semester` tinyint(1) DEFAULT NULL,
  `stud_id` int(11) DEFAULT NULL,
  `prof_id` int(11) DEFAULT NULL,
  `sub_id` int(11) DEFAULT NULL,
  `grade` decimal(5,2) DEFAULT NULL,
  `attendance` varchar(20) DEFAULT NULL,
  `quiz` varchar(20) DEFAULT NULL,
  `recitation` varchar(20) DEFAULT NULL,
  `project` varchar(20) DEFAULT NULL,
  `prelim` varchar(20) DEFAULT NULL,
  `midterm` varchar(20) DEFAULT NULL,
  `semis` varchar(20) DEFAULT NULL,
  `finals` varchar(20) DEFAULT NULL,
  `remark` varchar(20) DEFAULT NULL,
  `numerical` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `grades`
--

INSERT INTO `grades` (`grades_id`, `school_year`, `semester`, `stud_id`, `prof_id`, `sub_id`, `grade`, `attendance`, `quiz`, `recitation`, `project`, `prelim`, `midterm`, `semis`, `finals`, `remark`, `numerical`) VALUES
(4, '2026-2027', 1, 7, 1, 5, '39.45', '0.30', '0.40', '0.30', '0.20', '11.25', '10.50', '9.00', '7.50', 'Passed', '5.0');

-- --------------------------------------------------------

--
-- Table structure for table `grade_submission`
--

CREATE TABLE `grade_submission` (
  `id` int(11) NOT NULL,
  `prof_id` int(11) NOT NULL,
  `school_year` varchar(20) NOT NULL,
  `semester` tinyint(1) NOT NULL,
  `submitted` tinyint(1) NOT NULL DEFAULT 0,
  `submitted_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `grade_submission`
--

INSERT INTO `grade_submission` (`id`, `prof_id`, `school_year`, `semester`, `submitted`, `submitted_at`) VALUES
(1, 1, '2026-2027', 1, 1, '2026-03-03 11:22:25');

-- --------------------------------------------------------

--
-- Table structure for table `prof`
--

CREATE TABLE `prof` (
  `prof_id` int(11) NOT NULL,
  `acc_id` int(11) DEFAULT NULL,
  `firstname` varchar(20) DEFAULT NULL,
  `middlename` varchar(20) DEFAULT NULL,
  `lastname` varchar(20) DEFAULT NULL,
  `subject` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `gender` varchar(20) DEFAULT NULL,
  `status` enum('active','inactive') NOT NULL DEFAULT 'active',
  `role` varchar(20) DEFAULT NULL,
  `image_path` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prof`
--

INSERT INTO `prof` (`prof_id`, `acc_id`, `firstname`, `middlename`, `lastname`, `subject`, `email`, `gender`, `status`, `role`, `image_path`) VALUES
(1, 8, 'Darie', 'Milano', 'Kafka', NULL, 'Dar@gmail.com', 'female', 'active', 'teacher', ''),
(2, 11, 'haliburton', 'james', 'heli', NULL, 'kei@gmail.com', 'male', 'active', 'teacher', NULL),
(3, 12, 'lebron', 'curry', 'james', NULL, 'lebron@gmail.com', 'male', 'active', 'teacher', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `profsectionsubject`
--

CREATE TABLE `profsectionsubject` (
  `id` int(11) NOT NULL,
  `prof_id` int(11) DEFAULT NULL,
  `sub_id` int(11) DEFAULT NULL,
  `section_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `profsectionsubject`
--

INSERT INTO `profsectionsubject` (`id`, `prof_id`, `sub_id`, `section_id`) VALUES
(7, 1, 5, 4),
(8, 2, 43, 5),
(9, 3, 43, 5);

-- --------------------------------------------------------

--
-- Table structure for table `section`
--

CREATE TABLE `section` (
  `section_id` int(11) NOT NULL,
  `year_lvl` int(11) DEFAULT NULL,
  `section` varchar(50) DEFAULT NULL,
  `course_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `section`
--

INSERT INTO `section` (`section_id`, `year_lvl`, `section`, `course_id`) VALUES
(5, 1, 'BSA 1-1', 3),
(4, 1, 'BSIT 1-1', 1);

-- --------------------------------------------------------

--
-- Table structure for table `sem_control`
--

CREATE TABLE `sem_control` (
  `id` int(11) NOT NULL,
  `school_year` varchar(20) NOT NULL DEFAULT '2024-2025',
  `semester` tinyint(1) NOT NULL DEFAULT 1,
  `is_locked` tinyint(1) NOT NULL DEFAULT 0,
  `date_started` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sem_control`
--

INSERT INTO `sem_control` (`id`, `school_year`, `semester`, `is_locked`, `date_started`) VALUES
(1, '2026-2027', 1, 0, '2026-02-18 09:23:33');

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `stud_id` int(11) NOT NULL,
  `acc_id` int(11) DEFAULT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `middlename` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `gender` varchar(20) DEFAULT NULL,
  `email` varchar(250) DEFAULT NULL,
  `course` varchar(100) DEFAULT NULL,
  `yr_lvl` int(11) DEFAULT NULL,
  `role` varchar(20) DEFAULT NULL,
  `image_path` varchar(100) DEFAULT NULL,
  `section_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`stud_id`, `acc_id`, `firstname`, `middlename`, `lastname`, `gender`, `email`, `course`, `yr_lvl`, `role`, `image_path`, `section_id`) VALUES
(7, 9, 'lei', 'dumangas', 'librora', 'male', 'lei@gmail.com', 'BSIT', 1, 'student', '78e34990fdae224049ca7a76e55c1efa.jpg', 4),
(8, 10, 'lei andri', 'dumangas', 'librora', 'male', 'lelei@gmail.com', 'BSA', 1, 'student', NULL, 5);

-- --------------------------------------------------------

--
-- Table structure for table `subject`
--

CREATE TABLE `subject` (
  `sub_id` int(11) NOT NULL,
  `sub_code` varchar(20) DEFAULT NULL,
  `sub_name` varchar(100) DEFAULT NULL,
  `course_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `subject`
--

INSERT INTO `subject` (`sub_id`, `sub_code`, `sub_name`, `course_id`) VALUES
(1, 'MITE10', 'ComputerLog', 1),
(2, 'MITE1', 'Ai', 1),
(3, 'MITE2', 'LANDPRO', 1),
(4, 'MITE3', 'CDO', 1),
(5, 'MITE4', 'MGM', 1),
(6, 'MITE5', 'LEarnHTML', 1),
(7, 'MITE6', 'HAcK', 1),
(8, 'MITE7', 'SQU', 1),
(9, 'MITE8', 'SUO', 1),
(10, 'MITE9', 'SUDO', 1),
(11, 'BSIT101', 'Web Development Fundamentals', 1),
(12, 'BSIT102', 'Mobile App Development', 1),
(13, 'BSIT103', 'Systems Analysis and Design', 1),
(14, 'BSIT104', 'IT Project Management', 1),
(15, 'BSIT105', 'Cybersecurity Basics', 1),
(16, 'CS101', 'Introduction to Programming', 2),
(17, 'CS102', 'Data Structures and Algorithms', 2),
(18, 'CS103', 'Object-Oriented Programming', 2),
(19, 'CS104', 'Discrete Mathematics', 2),
(20, 'CS105', 'Computer Architecture', 2),
(21, 'CS106', 'Operating Systems', 2),
(22, 'CS107', 'Database Management Systems', 2),
(23, 'CS108', 'Computer Networks', 2),
(24, 'CS109', 'Software Engineering', 2),
(25, 'CS110', 'Artificial Intelligence', 2),
(26, 'CS111', 'Machine Learning', 2),
(27, 'CS112', 'Theory of Computation', 2),
(28, 'CS113', 'Compiler Design', 2),
(29, 'CS114', 'Computer Graphics', 2),
(30, 'CS115', 'Capstone Project', 2),
(31, 'ACC101', 'Financial Accounting 1', 3),
(32, 'ACC102', 'Financial Accounting 2', 3),
(33, 'ACC103', 'Managerial Accounting', 3),
(34, 'ACC104', 'Cost Accounting', 3),
(35, 'ACC105', 'Auditing Theory', 3),
(36, 'ACC106', 'Tax Accounting', 3),
(37, 'ACC107', 'Advanced Financial Accounting', 3),
(38, 'ACC108', 'Government Accounting', 3),
(39, 'ACC109', 'Accounting Information Systems', 3),
(40, 'ACC110', 'Financial Management', 3),
(41, 'ACC111', 'Corporate Finance', 3),
(42, 'ACC112', 'Business Law', 3),
(43, 'ACC113', 'Forensic Accounting', 3),
(44, 'ACC114', 'International Accounting', 3),
(45, 'ACC115', 'Accounting Research', 3),
(46, 'NUR101', 'Fundamentals of Nursing', 4),
(47, 'NUR102', 'Anatomy and Physiology', 4),
(48, 'NUR103', 'Medical-Surgical Nursing 1', 4),
(49, 'NUR104', 'Medical-Surgical Nursing 2', 4),
(50, 'NUR105', 'Maternal and Child Nursing', 4),
(51, 'NUR106', 'Pediatric Nursing', 4),
(52, 'NUR107', 'Mental Health Nursing', 4),
(53, 'NUR108', 'Community Health Nursing', 4),
(54, 'NUR109', 'Pharmacology', 4),
(55, 'NUR110', 'Pathophysiology', 4),
(56, 'NUR111', 'Nursing Research', 4),
(57, 'NUR112', 'Health Assessment', 4),
(58, 'NUR113', 'Critical Care Nursing', 4),
(59, 'NUR114', 'Geriatric Nursing', 4),
(60, 'NUR115', 'Nursing Leadership and Management', 4),
(61, 'PSY101', 'Introduction to Psychology', 5),
(62, 'PSY102', 'Developmental Psychology', 5),
(63, 'PSY103', 'Social Psychology', 5),
(64, 'PSY104', 'Abnormal Psychology', 5),
(65, 'PSY105', 'Cognitive Psychology', 5),
(66, 'PSY106', 'Theories of Personality', 5),
(67, 'PSY107', 'Psychological Assessment', 5),
(68, 'PSY108', 'Research Methods in Psychology', 5),
(69, 'PSY109', 'Industrial-Organizational Psychology', 5),
(70, 'PSY110', 'Counseling Psychology', 5),
(71, 'HRM101', 'Introduction to Hospitality Management', 6),
(72, 'HRM102', 'Food and Beverage Service', 6),
(73, 'HRM103', 'Kitchen Management', 6),
(74, 'HRM104', 'Front Office Operations', 6),
(75, 'HRM105', 'Housekeeping Management', 6),
(76, 'HRM106', 'Hospitality Marketing', 6),
(77, 'HRM107', 'Events Management', 6),
(78, 'HRM108', 'Menu Planning and Design', 6),
(79, 'HRM109', 'Hospitality Law', 6),
(80, 'HRM110', 'Strategic Management in Hospitality', 6),
(81, 'BSED101', 'The Teaching Profession', 7),
(82, 'BSED102', 'Child and Adolescent Development', 7),
(83, 'BSED103', 'Principles of Teaching', 7),
(84, 'BSED104', 'Assessment in Learning', 7),
(85, 'BSED105', 'Educational Technology', 7),
(86, 'BSED106', 'Curriculum Development', 7),
(87, 'BSED107', 'Classroom Management', 7),
(88, 'BSED108', 'Teaching English', 7),
(89, 'BSED109', 'Teaching Mathematics', 7),
(90, 'BSED110', 'Field Study and Practice Teaching', 7),
(91, 'BEED101', 'The Child and Adolescent Learners', 8),
(92, 'BEED102', 'Facilitating Learner-Centered Teaching', 8),
(93, 'BEED103', 'Teaching Science in Elementary', 8),
(94, 'BEED104', 'Teaching Math in Elementary', 8),
(95, 'BEED105', 'Teaching Reading and Writing', 8),
(96, 'BEED106', 'Teaching Social Studies', 8),
(97, 'BEED107', 'Technology for Teaching and Learning', 8),
(98, 'BEED108', 'Assessment of Learning', 8),
(99, 'BEED109', 'Building and Enhancing Literacy', 8),
(100, 'BEED110', 'Practice Teaching', 8),
(101, 'ECE101', 'Circuit Analysis', 9),
(102, 'ECE102', 'Electronics 1', 9),
(103, 'ECE103', 'Electronics 2', 9),
(104, 'ECE104', 'Digital Electronics', 9),
(105, 'ECE105', 'Signals and Systems', 9),
(106, 'ECE106', 'Communication Systems', 9),
(107, 'ECE107', 'Microprocessors and Microcontrollers', 9),
(108, 'ECE108', 'Electromagnetic Theory', 9),
(109, 'ECE109', 'Control Systems', 9),
(110, 'ECE110', 'Capstone Design Project', 9),
(111, 'CE101', 'Engineering Mechanics', 10),
(112, 'CE102', 'Surveying', 10),
(113, 'CE103', 'Strength of Materials', 10),
(114, 'CE104', 'Structural Analysis', 10),
(115, 'CE105', 'Hydraulics', 10),
(116, 'CE106', 'Transportation Engineering', 10),
(117, 'CE107', 'Geotechnical Engineering', 10),
(118, 'CE108', 'Construction Management', 10),
(119, 'CE109', 'Concrete Technology', 10),
(120, 'CE110', 'Capstone Project', 10),
(121, 'ME101', 'Statics of Rigid Bodies', 11),
(122, 'ME102', 'Dynamics of Rigid Bodies', 11),
(123, 'ME103', 'Thermodynamics', 11),
(124, 'ME104', 'Fluid Mechanics', 11),
(125, 'ME105', 'Machine Design', 11),
(126, 'ME106', 'Heat Transfer', 11),
(127, 'ME107', 'Manufacturing Processes', 11),
(128, 'ME108', 'Mechanics of Materials', 11),
(129, 'ME109', 'Control Systems', 11),
(130, 'ME110', 'Capstone Design Project', 11),
(131, 'EE101', 'Basic Electrical Engineering', 12),
(132, 'EE102', 'Circuit Theory', 12),
(133, 'EE103', 'Electrical Machines 1', 12),
(134, 'EE104', 'Electrical Machines 2', 12),
(135, 'EE105', 'Power Systems', 12),
(136, 'EE106', 'Electronics', 12),
(137, 'EE107', 'Control Systems', 12),
(138, 'EE108', 'Electromagnetic Fields', 12),
(139, 'EE109', 'Power Electronics', 12),
(140, 'EE110', 'Electrical Design Project', 12),
(141, 'CPE101', 'Computer Programming 1', 13),
(142, 'CPE102', 'Computer Programming 2', 13),
(143, 'CPE103', 'Digital Logic Design', 13),
(144, 'CPE104', 'Microprocessor Systems', 13),
(145, 'CPE105', 'Computer Architecture', 13),
(146, 'CPE106', 'Data Structures and Algorithms', 13),
(147, 'CPE107', 'Operating Systems', 13),
(148, 'CPE108', 'Computer Networks', 13),
(149, 'CPE109', 'Embedded Systems', 13),
(150, 'CPE110', 'Capstone Project', 13),
(151, 'TM101', 'Introduction to Tourism', 14),
(152, 'TM102', 'Tour Guiding', 14),
(153, 'TM103', 'Travel and Tour Operations', 14),
(154, 'TM104', 'Tourism Marketing', 14),
(155, 'TM105', 'Sustainable Tourism', 14),
(156, 'TM106', 'Philippine Tourism Geography', 14),
(157, 'TM107', 'Tourism Planning and Development', 14),
(158, 'TM108', 'Transportation Management', 14),
(159, 'TM109', 'Meetings, Incentives, Conventions and Exhibitions', 14),
(160, 'TM110', 'Tourism Research', 14),
(161, 'BA101', 'Principles of Management', 15),
(162, 'BA102', 'Business Mathematics', 15),
(163, 'BA103', 'Marketing Management', 15),
(164, 'BA104', 'Financial Management', 15),
(165, 'BA105', 'Human Resource Management', 15),
(166, 'BA106', 'Operations Management', 15),
(167, 'BA107', 'Business Law', 15),
(168, 'BA108', 'Organizational Behavior', 15),
(169, 'BA109', 'Entrepreneurship', 15),
(170, 'BA110', 'Strategic Management', 15);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`acc_id`);

--
-- Indexes for table `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`course_id`);

--
-- Indexes for table `grades`
--
ALTER TABLE `grades`
  ADD PRIMARY KEY (`grades_id`),
  ADD KEY `fk_prof_grades` (`prof_id`),
  ADD KEY `fk_stud_grades` (`stud_id`),
  ADD KEY `fk_sub_grades` (`sub_id`);

--
-- Indexes for table `grade_submission`
--
ALTER TABLE `grade_submission`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `uq_prof_sem` (`prof_id`,`school_year`,`semester`);

--
-- Indexes for table `prof`
--
ALTER TABLE `prof`
  ADD PRIMARY KEY (`prof_id`),
  ADD KEY `fk_prof_id` (`acc_id`);

--
-- Indexes for table `profsectionsubject`
--
ALTER TABLE `profsectionsubject`
  ADD PRIMARY KEY (`id`),
  ADD KEY `prof_id` (`prof_id`),
  ADD KEY `sub_id` (`sub_id`),
  ADD KEY `section_id` (`section_id`);

--
-- Indexes for table `section`
--
ALTER TABLE `section`
  ADD PRIMARY KEY (`section_id`),
  ADD UNIQUE KEY `uq_section_name` (`section`,`year_lvl`,`course_id`),
  ADD KEY `fk_section_course` (`course_id`);

--
-- Indexes for table `sem_control`
--
ALTER TABLE `sem_control`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`stud_id`),
  ADD KEY `fk_stud_id` (`acc_id`),
  ADD KEY `fk_section_id` (`section_id`);

--
-- Indexes for table `subject`
--
ALTER TABLE `subject`
  ADD PRIMARY KEY (`sub_id`),
  ADD KEY `fk_subject_course` (`course_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `acc_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `course`
--
ALTER TABLE `course`
  MODIFY `course_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `grades`
--
ALTER TABLE `grades`
  MODIFY `grades_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `grade_submission`
--
ALTER TABLE `grade_submission`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `prof`
--
ALTER TABLE `prof`
  MODIFY `prof_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `profsectionsubject`
--
ALTER TABLE `profsectionsubject`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `section`
--
ALTER TABLE `section`
  MODIFY `section_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `sem_control`
--
ALTER TABLE `sem_control`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `stud_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `subject`
--
ALTER TABLE `subject`
  MODIFY `sub_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=171;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `grades`
--
ALTER TABLE `grades`
  ADD CONSTRAINT `fk_prof_grades` FOREIGN KEY (`prof_id`) REFERENCES `prof` (`prof_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_stud_grades` FOREIGN KEY (`stud_id`) REFERENCES `student` (`stud_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_sub_grades` FOREIGN KEY (`sub_id`) REFERENCES `subject` (`sub_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `grade_submission`
--
ALTER TABLE `grade_submission`
  ADD CONSTRAINT `grade_submission_ibfk_1` FOREIGN KEY (`prof_id`) REFERENCES `prof` (`prof_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `prof`
--
ALTER TABLE `prof`
  ADD CONSTRAINT `fk_prof_id` FOREIGN KEY (`acc_id`) REFERENCES `account` (`acc_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `profsectionsubject`
--
ALTER TABLE `profsectionsubject`
  ADD CONSTRAINT `profsectionsubject_ibfk_1` FOREIGN KEY (`prof_id`) REFERENCES `prof` (`prof_id`),
  ADD CONSTRAINT `profsectionsubject_ibfk_2` FOREIGN KEY (`sub_id`) REFERENCES `subject` (`sub_id`),
  ADD CONSTRAINT `profsectionsubject_ibfk_3` FOREIGN KEY (`section_id`) REFERENCES `section` (`section_id`);

--
-- Constraints for table `section`
--
ALTER TABLE `section`
  ADD CONSTRAINT `fk_section_course` FOREIGN KEY (`course_id`) REFERENCES `course` (`course_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Constraints for table `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `fk_section_id` FOREIGN KEY (`section_id`) REFERENCES `section` (`section_id`),
  ADD CONSTRAINT `fk_stud_id` FOREIGN KEY (`acc_id`) REFERENCES `account` (`acc_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `subject`
--
ALTER TABLE `subject`
  ADD CONSTRAINT `fk_subject_course` FOREIGN KEY (`course_id`) REFERENCES `course` (`course_id`) ON DELETE SET NULL ON UPDATE CASCADE;
--
-- Database: `phpmyadmin`
--
CREATE DATABASE IF NOT EXISTS `phpmyadmin` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin;
USE `phpmyadmin`;

-- --------------------------------------------------------

--
-- Table structure for table `pma__bookmark`
--

CREATE TABLE `pma__bookmark` (
  `id` int(10) UNSIGNED NOT NULL,
  `dbase` varchar(255) NOT NULL DEFAULT '',
  `user` varchar(255) NOT NULL DEFAULT '',
  `label` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `query` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Bookmarks';

-- --------------------------------------------------------

--
-- Table structure for table `pma__central_columns`
--

CREATE TABLE `pma__central_columns` (
  `db_name` varchar(64) NOT NULL,
  `col_name` varchar(64) NOT NULL,
  `col_type` varchar(64) NOT NULL,
  `col_length` text DEFAULT NULL,
  `col_collation` varchar(64) NOT NULL,
  `col_isNull` tinyint(1) NOT NULL,
  `col_extra` varchar(255) DEFAULT '',
  `col_default` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Central list of columns';

-- --------------------------------------------------------

--
-- Table structure for table `pma__column_info`
--

CREATE TABLE `pma__column_info` (
  `id` int(5) UNSIGNED NOT NULL,
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `column_name` varchar(64) NOT NULL DEFAULT '',
  `comment` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `mimetype` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `transformation` varchar(255) NOT NULL DEFAULT '',
  `transformation_options` varchar(255) NOT NULL DEFAULT '',
  `input_transformation` varchar(255) NOT NULL DEFAULT '',
  `input_transformation_options` varchar(255) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Column information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__designer_settings`
--

CREATE TABLE `pma__designer_settings` (
  `username` varchar(64) NOT NULL,
  `settings_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Settings related to Designer';

-- --------------------------------------------------------

--
-- Table structure for table `pma__export_templates`
--

CREATE TABLE `pma__export_templates` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL,
  `export_type` varchar(10) NOT NULL,
  `template_name` varchar(64) NOT NULL,
  `template_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved export templates';

--
-- Dumping data for table `pma__export_templates`
--

INSERT INTO `pma__export_templates` (`id`, `username`, `export_type`, `template_name`, `template_data`) VALUES
(1, 'root', 'server', 'leleies', '{\"quick_or_custom\":\"quick\",\"what\":\"sql\",\"db_select[]\":[\"grading_system\",\"phpmyadmin\",\"test\"],\"aliases_new\":\"\",\"output_format\":\"sendit\",\"filename_template\":\"@SERVER@\",\"remember_template\":\"on\",\"charset\":\"utf-8\",\"compression\":\"none\",\"maxsize\":\"\",\"codegen_structure_or_data\":\"data\",\"codegen_format\":\"0\",\"csv_separator\":\",\",\"csv_enclosed\":\"\\\"\",\"csv_escaped\":\"\\\"\",\"csv_terminated\":\"AUTO\",\"csv_null\":\"NULL\",\"csv_structure_or_data\":\"data\",\"excel_null\":\"NULL\",\"excel_columns\":\"something\",\"excel_edition\":\"win\",\"excel_structure_or_data\":\"data\",\"json_structure_or_data\":\"data\",\"json_unicode\":\"something\",\"latex_caption\":\"something\",\"latex_structure_or_data\":\"structure_and_data\",\"latex_structure_caption\":\"Structure of table @TABLE@\",\"latex_structure_continued_caption\":\"Structure of table @TABLE@ (continued)\",\"latex_structure_label\":\"tab:@TABLE@-structure\",\"latex_relation\":\"something\",\"latex_comments\":\"something\",\"latex_mime\":\"something\",\"latex_columns\":\"something\",\"latex_data_caption\":\"Content of table @TABLE@\",\"latex_data_continued_caption\":\"Content of table @TABLE@ (continued)\",\"latex_data_label\":\"tab:@TABLE@-data\",\"latex_null\":\"\\\\textit{NULL}\",\"mediawiki_structure_or_data\":\"data\",\"mediawiki_caption\":\"something\",\"mediawiki_headers\":\"something\",\"htmlword_structure_or_data\":\"structure_and_data\",\"htmlword_null\":\"NULL\",\"ods_null\":\"NULL\",\"ods_structure_or_data\":\"data\",\"odt_structure_or_data\":\"structure_and_data\",\"odt_relation\":\"something\",\"odt_comments\":\"something\",\"odt_mime\":\"something\",\"odt_columns\":\"something\",\"odt_null\":\"NULL\",\"pdf_report_title\":\"\",\"pdf_structure_or_data\":\"data\",\"phparray_structure_or_data\":\"data\",\"sql_include_comments\":\"something\",\"sql_header_comment\":\"\",\"sql_use_transaction\":\"something\",\"sql_compatibility\":\"NONE\",\"sql_structure_or_data\":\"structure_and_data\",\"sql_create_table\":\"something\",\"sql_auto_increment\":\"something\",\"sql_create_view\":\"something\",\"sql_create_trigger\":\"something\",\"sql_backquotes\":\"something\",\"sql_type\":\"INSERT\",\"sql_insert_syntax\":\"both\",\"sql_max_query_size\":\"50000\",\"sql_hex_for_binary\":\"something\",\"sql_utc_time\":\"something\",\"texytext_structure_or_data\":\"structure_and_data\",\"texytext_null\":\"NULL\",\"yaml_structure_or_data\":\"data\",\"\":null,\"as_separate_files\":null,\"csv_removeCRLF\":null,\"csv_columns\":null,\"excel_removeCRLF\":null,\"json_pretty_print\":null,\"htmlword_columns\":null,\"ods_columns\":null,\"sql_dates\":null,\"sql_relation\":null,\"sql_mime\":null,\"sql_disable_fk\":null,\"sql_views_as_tables\":null,\"sql_metadata\":null,\"sql_drop_database\":null,\"sql_drop_table\":null,\"sql_if_not_exists\":null,\"sql_simple_view_export\":null,\"sql_view_current_user\":null,\"sql_or_replace_view\":null,\"sql_procedure_function\":null,\"sql_truncate\":null,\"sql_delayed\":null,\"sql_ignore\":null,\"texytext_columns\":null}');

-- --------------------------------------------------------

--
-- Table structure for table `pma__favorite`
--

CREATE TABLE `pma__favorite` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Favorite tables';

-- --------------------------------------------------------

--
-- Table structure for table `pma__history`
--

CREATE TABLE `pma__history` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db` varchar(64) NOT NULL DEFAULT '',
  `table` varchar(64) NOT NULL DEFAULT '',
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp(),
  `sqlquery` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='SQL history for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__navigationhiding`
--

CREATE TABLE `pma__navigationhiding` (
  `username` varchar(64) NOT NULL,
  `item_name` varchar(64) NOT NULL,
  `item_type` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Hidden items of navigation tree';

-- --------------------------------------------------------

--
-- Table structure for table `pma__pdf_pages`
--

CREATE TABLE `pma__pdf_pages` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `page_nr` int(10) UNSIGNED NOT NULL,
  `page_descr` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='PDF relation pages for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__recent`
--

CREATE TABLE `pma__recent` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Recently accessed tables';

--
-- Dumping data for table `pma__recent`
--

INSERT INTO `pma__recent` (`username`, `tables`) VALUES
('root', '[{\"db\":\"grading_system\",\"table\":\"prof\"},{\"db\":\"grading_system\",\"table\":\"student\"},{\"db\":\"grading_system\",\"table\":\"subject\"},{\"db\":\"grading_system\",\"table\":\"profsectionsubject\"},{\"db\":\"grading_system\",\"table\":\"account\"},{\"db\":\"grading_system\",\"table\":\"section\"}]');

-- --------------------------------------------------------

--
-- Table structure for table `pma__relation`
--

CREATE TABLE `pma__relation` (
  `master_db` varchar(64) NOT NULL DEFAULT '',
  `master_table` varchar(64) NOT NULL DEFAULT '',
  `master_field` varchar(64) NOT NULL DEFAULT '',
  `foreign_db` varchar(64) NOT NULL DEFAULT '',
  `foreign_table` varchar(64) NOT NULL DEFAULT '',
  `foreign_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Relation table';

-- --------------------------------------------------------

--
-- Table structure for table `pma__savedsearches`
--

CREATE TABLE `pma__savedsearches` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `search_name` varchar(64) NOT NULL DEFAULT '',
  `search_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved searches';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_coords`
--

CREATE TABLE `pma__table_coords` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `pdf_page_number` int(11) NOT NULL DEFAULT 0,
  `x` float UNSIGNED NOT NULL DEFAULT 0,
  `y` float UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table coordinates for phpMyAdmin PDF output';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_info`
--

CREATE TABLE `pma__table_info` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `display_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_uiprefs`
--

CREATE TABLE `pma__table_uiprefs` (
  `username` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `prefs` text NOT NULL,
  `last_update` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Tables'' UI preferences';

--
-- Dumping data for table `pma__table_uiprefs`
--

INSERT INTO `pma__table_uiprefs` (`username`, `db_name`, `table_name`, `prefs`, `last_update`) VALUES
('root', 'grading_system', 'prof', '{\"sorted_col\":\"`prof`.`image_path` ASC\"}', '2026-04-11 03:17:45');

-- --------------------------------------------------------

--
-- Table structure for table `pma__tracking`
--

CREATE TABLE `pma__tracking` (
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `version` int(10) UNSIGNED NOT NULL,
  `date_created` datetime NOT NULL,
  `date_updated` datetime NOT NULL,
  `schema_snapshot` text NOT NULL,
  `schema_sql` text DEFAULT NULL,
  `data_sql` longtext DEFAULT NULL,
  `tracking` set('UPDATE','REPLACE','INSERT','DELETE','TRUNCATE','CREATE DATABASE','ALTER DATABASE','DROP DATABASE','CREATE TABLE','ALTER TABLE','RENAME TABLE','DROP TABLE','CREATE INDEX','DROP INDEX','CREATE VIEW','ALTER VIEW','DROP VIEW') DEFAULT NULL,
  `tracking_active` int(1) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Database changes tracking for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__userconfig`
--

CREATE TABLE `pma__userconfig` (
  `username` varchar(64) NOT NULL,
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `config_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User preferences storage for phpMyAdmin';

--
-- Dumping data for table `pma__userconfig`
--

INSERT INTO `pma__userconfig` (`username`, `timevalue`, `config_data`) VALUES
('root', '2026-04-11 03:38:18', '{\"Console\\/Mode\":\"collapse\"}');

-- --------------------------------------------------------

--
-- Table structure for table `pma__usergroups`
--

CREATE TABLE `pma__usergroups` (
  `usergroup` varchar(64) NOT NULL,
  `tab` varchar(64) NOT NULL,
  `allowed` enum('Y','N') NOT NULL DEFAULT 'N'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User groups with configured menu items';

-- --------------------------------------------------------

--
-- Table structure for table `pma__users`
--

CREATE TABLE `pma__users` (
  `username` varchar(64) NOT NULL,
  `usergroup` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Users and their assignments to user groups';

--
-- Indexes for dumped tables
--

--
-- Indexes for table `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pma__central_columns`
--
ALTER TABLE `pma__central_columns`
  ADD PRIMARY KEY (`db_name`,`col_name`);

--
-- Indexes for table `pma__column_info`
--
ALTER TABLE `pma__column_info`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `db_name` (`db_name`,`table_name`,`column_name`);

--
-- Indexes for table `pma__designer_settings`
--
ALTER TABLE `pma__designer_settings`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_user_type_template` (`username`,`export_type`,`template_name`);

--
-- Indexes for table `pma__favorite`
--
ALTER TABLE `pma__favorite`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__history`
--
ALTER TABLE `pma__history`
  ADD PRIMARY KEY (`id`),
  ADD KEY `username` (`username`,`db`,`table`,`timevalue`);

--
-- Indexes for table `pma__navigationhiding`
--
ALTER TABLE `pma__navigationhiding`
  ADD PRIMARY KEY (`username`,`item_name`,`item_type`,`db_name`,`table_name`);

--
-- Indexes for table `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  ADD PRIMARY KEY (`page_nr`),
  ADD KEY `db_name` (`db_name`);

--
-- Indexes for table `pma__recent`
--
ALTER TABLE `pma__recent`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__relation`
--
ALTER TABLE `pma__relation`
  ADD PRIMARY KEY (`master_db`,`master_table`,`master_field`),
  ADD KEY `foreign_field` (`foreign_db`,`foreign_table`);

--
-- Indexes for table `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_savedsearches_username_dbname` (`username`,`db_name`,`search_name`);

--
-- Indexes for table `pma__table_coords`
--
ALTER TABLE `pma__table_coords`
  ADD PRIMARY KEY (`db_name`,`table_name`,`pdf_page_number`);

--
-- Indexes for table `pma__table_info`
--
ALTER TABLE `pma__table_info`
  ADD PRIMARY KEY (`db_name`,`table_name`);

--
-- Indexes for table `pma__table_uiprefs`
--
ALTER TABLE `pma__table_uiprefs`
  ADD PRIMARY KEY (`username`,`db_name`,`table_name`);

--
-- Indexes for table `pma__tracking`
--
ALTER TABLE `pma__tracking`
  ADD PRIMARY KEY (`db_name`,`table_name`,`version`);

--
-- Indexes for table `pma__userconfig`
--
ALTER TABLE `pma__userconfig`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__usergroups`
--
ALTER TABLE `pma__usergroups`
  ADD PRIMARY KEY (`usergroup`,`tab`,`allowed`);

--
-- Indexes for table `pma__users`
--
ALTER TABLE `pma__users`
  ADD PRIMARY KEY (`username`,`usergroup`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__column_info`
--
ALTER TABLE `pma__column_info`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `pma__history`
--
ALTER TABLE `pma__history`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  MODIFY `page_nr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- Database: `test`
--
CREATE DATABASE IF NOT EXISTS `test` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `test`;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

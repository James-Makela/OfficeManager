-- --------------------------------------------------------------------
-- Filename:   JIM-ICTPRG432-AT2-POR-PtN.sql
-- Author:     JAMES MAKELA
-- Email:      20112895@tafe.wa.edu.au
-- --------------------------------------------------------------------
-- Purpose:   
--    This file contains the SQL used to create and execute 
--    the solutions for the assessment ICTPRG402 Portfolio
-- --------------------------------------------------------------------
-- Declaration:
--    By completing and submitting this assessment 
--    via the Blackboard LMS or other methods, to my 
--    lecturer, I am stating that: 
--      * The attached submission is completely own work 
--      * I have correctly indicated all sources of information
--        used in this work (if required) 
--      * I have kept a copy of this assessment (where practicable)
--      * I understand a copy of my assessment will be kept by
--        NMTAFE for their records 
--      * I understand my assessment may be selected for use in
--        NMTAFE’s validation and audit process to ensure student 
--        assessment is valid and meets requirements of the unit 
--        of competency
-- --------------------------------------------------------------------

-- --------------------------------------------------------------------

SELECT * FROM employees;

-- --------------------------------------------------------------------

INSERT INTO employees (
                        given_name,
                        family_name,
                        date_of_birth,
                        gender_identity,
                        gross_salary,
                        supervisor_id,
                        branch_id,
                        created_at) 
                    VALUES
                        ('{0}', '{1}', ""{2}"", '{3}', {4}, {5}, {6}, ""{7}"");

-- --------------------------------------------------------------------

UPDATE employees
                    SET 
                        given_name='{0}',
                        family_name='{1}',
                        date_of_birth=""{2}"",
                        gender_identity='{3}',
                        gross_salary={4},
                        supervisor_id={5},
                        branch_id={6},
                        updated_at=""{7}""
                    WHERE 
                        id={8};

-- --------------------------------------------------------------------

SELECT * FROM employees
                    WHERE given_name LIKE ""%{0}%"";

-- --------------------------------------------------------------------

SELECT * FROM employees
                    WHERE branch_id={0};

-- --------------------------------------------------------------------

SELECT * FROM employees
                    WHERE gross_salary>={0};

-- --------------------------------------------------------------------

DELETE FROM employees WHERE id={0};

-- --------------------------------------------------------------------

SELECT 
        employees.id,
        CONCAT(given_name, "" "", family_name),
        client_name,
        total_sales
FROM employees
JOIN working_with ON employees.id=employee_id
JOIN clients ON clients.id=working_with.client_id
ORDER BY employees.id;
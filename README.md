# Asp.Net MVC Identity - Permission Based Authorization
## Adds permission based authorization capability on Asp.Net MVC Identity Framework

## Motivation
#### Problem
There are instances where role based authorization itself is not sufficient or not appropriate. One example is when the business team is not precise on the number of roles the application need to have, or the permissions a role need to have. Or, the business team keeps on changing permissions on the roles defined for an application.

In such situation, if the application was designed to use merely role based authorization, then the changes require additional support effort for development and operational support team.

#### One Solution
Permissions of an application don't change unless new features are added, or some features are removed. So, authorizing requests based on permission and providing the business team (or admin) the capability of assigning permissions to application users will help minimize the operational support effort.

As an example, the admin of an application creates a new role for an application, assigns certain permissions to the that role, and assigns the role to application users. Providing this capability to admin will address the changing part, i.e., the roles and accesses associated with the roles.

This framework provides a mechanism for authorizing resources based on permissions.

## Using this Framework

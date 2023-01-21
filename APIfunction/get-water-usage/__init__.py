import logging

import azure.functions as func
import json
import mysql.connector

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    user_id = req.params.get('user_id')
    if not user_id:
        try:
            req_body = req.get_json()
        except ValueError:
            pass
        else:
            user_id = req_body.get('user_id')



    if user_id:
        cnx = mysql.connector.connect(user='NWGadmin', password='P4Gjax5akzCBA2h',
                                      host='nwgsqlserver.mysql.database.azure.com',
                                      database='nwg')
        cursor = cnx.cursor()
        water_usage = {}

        water_usage["user_id"] = user_id
        query = f"select sum(usage_ml) from water_usage where created_at between CURDATE() and now() and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["today"] = cursor.fetchall()[0][0]

        query = f"select sum(usage_ml) from water_usage where created_at between Date_sub(CURDATE(), interval 1 day) and curdate() and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["yesterday"] = cursor.fetchall()[0][0]

        query = f"select sum(usage_ml) from water_usage where created_at between Date_sub(CURDATE(), interval (DAYOFWEEK(curdate()) + 5)%7 day) and now() and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["this_week"] = cursor.fetchall()[0][0]

        query = f"select sum(usage_ml) from water_usage where created_at between Date_sub(CURDATE(), interval (dayofmonth(curdate()) - 1) day) and now() and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["this_month"] = cursor.fetchall()[0][0]

        query = f"select sum(usage_ml) from water_usage where created_at between Date_sub(CURDATE(), interval (DAYOFWEEK(curdate()) + 5)%7 + 7 day) and date_sub(date_sub(CURDATE(), interval (DAYOFWEEK(curdate()) + 5)%7 day), interval 1 second) and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["last_week"] = cursor.fetchall()[0][0]

        query = f"select sum(usage_ml) from water_usage where created_at between Date_sub(Date_sub(CURDATE(), interval (dayofmonth(curdate()) - 1) day), interval 1 month) and Date_Sub(Date_sub(CURDATE(), interval (dayofmonth(curdate()) - 1) day), interval 1 second) and user_id = {user_id} group by user_id"
        cursor.execute(query)
        water_usage["last_month"] = cursor.fetchall()[0][0]


        return func.HttpResponse(json.dumps(water_usage))
    else:
        return func.HttpResponse(
             "This HTTP triggered function executed successfully. Pass a user_id in the query string or in the request body for a personalized response.",
             status_code=200
        )

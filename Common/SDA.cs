using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using System.Data.SQLite;

using SDM.Component;
using SDM.Project;

namespace SDM.Common
{
  /*
  http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki
  https://www.sqlite.org/doclist.html
  https://somjang.tistory.com/entry/SQLite3-%EC%84%A4%EC%B9%98%ED%95%98%EA%B8%B0
  https://sqlitebrowser.org/
  https://sqlitebrowser.org/dl/
  https://www.connectionstrings.com/sqlite/
  https://stackoverflow.com/questions/1381264/password-protect-a-sqlite-db-is-it-possible
  http://www.gisdeveloper.co.kr/?p=2290
  https://www.csharpstudy.com/Practical/Prac-sqlite.aspx
  */
  public class SDA
  {
    private string ConnectionString;

    public SDA()
    {
      //this.ConnectionString = string.Format(@"Data Source={0}", SCommon.DBFilePath);
      //this.ConnectionString = string.Format(@"Data Source={0};Version=3;Password=MSAModeling;Pooling=True;Max Pool Size=100;", SCommon.DBFilePath);
      this.ConnectionString = string.Format(@"Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", SCommon.DBFilePath);
    }

    #region Microservice

    public List<SMicroservice> SelectMicroserviceList(bool sortByName = true, bool onlyMine = false)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT M.GUID " +
          "      , M.ProgramID " +
          "	     , M.Name " +
          "      , M.BizPartGUID, P0.Name AS BizPartName " +
          "	     , M.Description " +
          "	     , M.SpecFile" +
          "      , IFNULL(N.ServiceGUID, 'Y') AS Display " +
          "	     , M.RegisteredDate,    M.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    M.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , M.LastModifiedDate,  M.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  M.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_MICROSERVICE M " +
          "        INNER JOIN TB_PART P0 ON M.BizPartGUID = P0.GUID " +
          "        INNER JOIN TB_PART P1 ON M.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON M.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON M.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON M.LastModifiedUserGUID = U2.GUID " +
          "        LEFT OUTER JOIN TB_NO_DISPLAY N ON M.GUID = N.ServiceGUID ";

        if (onlyMine)
          sql += "  WHERE DISPLAY = 'Y' ";//"  WHERE N.UserGUID IS NULL "

        if (sortByName)
          sql += " ORDER BY M.Name ";
        else
          sql += " ORDER BY M.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      List<SMicroservice> msList = new List<SMicroservice>();

      foreach (DataRow row in dt.Rows)
      {
        SMicroservice ms = new SMicroservice();
        ms.GUID = Convert.ToString(row["GUID"]);
        ms.ProgramID = Convert.ToString(row["ProgramID"]);
        ms.Name = Convert.ToString(row["Name"]);
        ms.BizPartGUID = Convert.ToString(row["BizPartGUID"]);
        ms.BizPartName = Convert.ToString(row["BizPartName"]);
        ms.Description = Convert.ToString(row["Description"]);
        ms.SpecFile = Convert.ToString(row["SpecFile"]);
        ms.Display = Convert.ToString(row["Display"]);

        ms.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        ms.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        ms.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        ms.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        ms.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        ms.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        ms.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        ms.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        ms.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        ms.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        msList.Add(ms);
      }

      return msList;
    }

    public DataTable SelectMicroserviceList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT M.GUID " +
          "      , M.BizPartGUID, P.Name AS BizPartName " +
          "      , M.ProgramID " +
          "      , M.Name " +
          "      , M.Description " +
          "      , (SELECT COUNT(*) FROM TB_BIZ_PACKAGE WHERE MicroserviceGUID = M.GUID) AS BPCount " +
          "      , (SELECT COUNT(*) FROM TB_API         WHERE MicroserviceGUID = M.GUID) AS APICount " +
          "      , (SELECT COUNT(*) FROM TB_PUBLISHER   WHERE MicroserviceGUID = M.GUID) AS PubCount " +
          "      , (SELECT COUNT(*) FROM TB_SUBSCRIBER  WHERE MicroserviceGUID = M.GUID) AS SubCount " +
          "      , (SELECT COUNT(*) FROM TB_OTHER       WHERE MicroserviceGUID = M.GUID) AS OtherCount " +
          "      , (SELECT COUNT(*) FROM TB_BIZ_RULE_OPERATION    WHERE MicroserviceGUID = M.GUID) AS BROpCount " +
          "      , (SELECT COUNT(*) FROM TB_DATA_ACCESS_OPERATION WHERE MicroserviceGUID = M.GUID) AS DAOpCount " +
          "      , (SELECT COUNT(*) FROM TB_DTO     WHERE MicroserviceGUID = M.GUID) AS DtoCount " +
          "      , (SELECT COUNT(*) FROM TB_ENTITY  WHERE MicroserviceGUID = M.GUID) AS EntityCount " +
          "      , (SELECT COUNT(*) FROM TB_UI      WHERE MicroserviceGUID = M.GUID) AS UICount " +
          "      , (SELECT COUNT(*) FROM TB_JOB     WHERE MicroserviceGUID = M.GUID) AS JobCount " +
          "   FROM TB_MICROSERVICE M " +
          "        INNER JOIN TB_PART P ON M.BizPartGUID = P.GUID " +
          "  WHERE M.Name LIKE @Name " +
          " ORDER BY M.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public SMicroservice SelectMicroservice(string guid)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT M.GUID " +
          "      , M.ProgramID " +
          "	     , M.Name " +
          "      , M.BizPartGUID, P0.Name as BizPartName " +
          "	     , M.Description " +
          "      , M.SpecFile " +
          "	     , M.RegisteredDate,    M.RegisteredPartGUID,   P1.Name as RegisteredPartName,    M.RegisteredUserGUID,   U1.Name as RegisteredUserName " +
          "	     , M.LastModifiedDate,  M.LastModifiedPartGUID, P2.Name as LastModifiedPartName,  M.LastModifiedUserGUID, U2.Name as LastModifiedUserName " +
          "	  FROM TB_MICROSERVICE M " +
          "        INNER JOIN TB_PART P0 on M.BizPartGUID = P0.GUID " +
          "        INNER JOIN TB_PART P1 on M.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 on M.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 on M.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 on M.LastModifiedUserGUID = U2.GUID " +
          "  WHERE M.GUID = @GUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        adapter.Fill(dt);
      }

      if (dt.Rows.Count == 0)
        return null;

      DataRow row = dt.Rows[0];
      SMicroservice ms = new SMicroservice();

      ms.GUID = Convert.ToString(row["GUID"]);
      ms.ProgramID = Convert.ToString(row["ProgramID"]);
      ms.Name = Convert.ToString(row["Name"]);
      ms.BizPartGUID = Convert.ToString(row["BizPartGUID"]);
      ms.BizPartName = Convert.ToString(row["BizPartName"]);
      ms.Description = Convert.ToString(row["Description"]);
      ms.SpecFile = Convert.ToString(row["SpecFile"]);

      ms.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
      ms.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
      ms.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
      ms.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
      ms.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

      ms.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
      ms.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
      ms.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
      ms.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
      ms.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

      return ms;
    }

    public SMicroservice InsertMicroservice()
    {
      SMicroservice ms = new SMicroservice();
      ms.GUID = Guid.NewGuid().ToString();
      ms.Name = "New Microservice";
      ms.BizPartGUID = SCommon.LoggedInUser.PartGUID;
      SCommon.SetDateDesigner(ms, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_MICROSERVICE VALUES " + 
          " (  @GUID " +
          "  , @ProgramID " +
          "  , @Name " +
          "  , @BizPartGUID " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ms.GUID);
        cmd.Parameters.AddWithValue("@ProgramID", ms.ProgramID);
        cmd.Parameters.AddWithValue("@Name", ms.Name);
        cmd.Parameters.AddWithValue("@BizPartGUID", ms.BizPartGUID);
        cmd.Parameters.AddWithValue("@Description", ms.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ms.SpecFile);
        this.SetRegistered(cmd, ms);
        this.SetLastModified(cmd, ms);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return ms;
    }

    public void UpdateMicroservice(SMicroservice ms)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_MICROSERVICE " +
          "    SET ProgramID = @ProgramID " +
          "      , Name = @Name " +
          "      , BizPartGUID = @BizPartGUID " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", ms.ProgramID);
        cmd.Parameters.AddWithValue("@Name", ms.Name);
        cmd.Parameters.AddWithValue("@BizPartGUID", ms.BizPartGUID);
        cmd.Parameters.AddWithValue("@Description", ms.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ms.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", ms.GUID);
        this.SetLastModified(cmd, ms);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateMicroserviceLastModifiedTemp(SMicroservice ms)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_MICROSERVICE " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ms.GUID);
        this.SetLastModified(cmd, ms);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteMicroservice(SMicroservice ms)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_MICROSERVICE WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ms.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Internal System

    public List<SInternalSystem> SelectInternalSystemList(bool sortByName, bool onlyMine)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT SI.GUID " +
          "      , SI.ProgramID " +
          "	     , SI.Name " +
          "	     , SI.Description " +
          "      , SI.SpecFile " +
          "      , IFNULL(N.ServiceGUID, 'Y') AS Display " +
          "	     , SI.RegisteredDate,    SI.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    SI.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , SI.LastModifiedDate,  SI.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  SI.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_INTERNAL_SYSTEM SI " +
          "        INNER JOIN TB_PART P1 ON SI.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON SI.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON SI.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON SI.LastModifiedUserGUID = U2.GUID " +
          "        LEFT OUTER JOIN TB_NO_DISPLAY N ON SI.GUID = N.ServiceGUID ";

        if (onlyMine)
          sql += "  WHERE DISPLAY = 'Y' ";

        if (sortByName)
          sql += " ORDER BY SI.Name ";
        else
          sql += " ORDER BY SI.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      List<SInternalSystem> siList = new List<SInternalSystem>();

      foreach (DataRow row in dt.Rows)
      {
        SInternalSystem si = new SInternalSystem();
        si.GUID = Convert.ToString(row["GUID"]);
        si.ProgramID = Convert.ToString(row["ProgramID"]);
        si.Name = Convert.ToString(row["Name"]);
        si.Description = Convert.ToString(row["Description"]);
        si.SpecFile = Convert.ToString(row["SpecFile"]);
        si.Display = Convert.ToString(row["Display"]);
        
        si.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        si.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        si.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        si.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        si.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        si.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        si.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        si.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        si.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        si.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        siList.Add(si);
      }

      return siList;
    }

    public SInternalSystem SelectInternalSystem(string guid)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT SI.GUID " +
          "      , SI.ProgramID " +
          "	     , SI.Name " +
          "	     , SI.Description " +
          "      , SI.SpecFile " +
          "	     , SI.RegisteredDate,    SI.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    SI.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , SI.LastModifiedDate,  SI.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  SI.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_INTERNAL_SYSTEM SI " +
          "        INNER JOIN TB_PART P1 ON SI.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON SI.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON SI.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON SI.LastModifiedUserGUID = U2.GUID " +
          "  WHERE SI.GUID = @GUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        adapter.Fill(dt);
      }

      if (dt.Rows.Count == 0)
        return null;

      DataRow row = dt.Rows[0];
      SInternalSystem si = new SInternalSystem();

      si.GUID = Convert.ToString(row["GUID"]);
      si.ProgramID = Convert.ToString(row["ProgramID"]);
      si.Name = Convert.ToString(row["Name"]);
      si.Description = Convert.ToString(row["Description"]);
      si.SpecFile = Convert.ToString(row["SpecFile"]);
       
      si.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
      si.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
      si.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
      si.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
      si.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
       
      si.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
      si.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
      si.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
      si.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
      si.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

      return si;
    }

    public SInternalSystem InsertInternalSystem()
    {
      SInternalSystem si = new SInternalSystem();
      si.GUID = Guid.NewGuid().ToString();
      si.Name = "New InternalSystem";
      SCommon.SetDateDesigner(si, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_INTERNAL_SYSTEM VALUES " +
          " (  @GUID " +
          "  , @ProgramID " +
          "  , @Name " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", si.GUID);
        cmd.Parameters.AddWithValue("@ProgramID", si.ProgramID);
        cmd.Parameters.AddWithValue("@Name", si.Name);
        cmd.Parameters.AddWithValue("@Description", si.Description);
        cmd.Parameters.AddWithValue("@SpecFile", si.SpecFile);
        this.SetRegistered(cmd, si);
        this.SetLastModified(cmd, si);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return si;
    }

    public void UpdateInternalSystem(SInternalSystem si)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_INTERNAL_SYSTEM " +
          "    SET ProgramID = @ProgramID " +
          "      , Name = @Name " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", si.ProgramID);
        cmd.Parameters.AddWithValue("@Name", si.Name);
        cmd.Parameters.AddWithValue("@Description", si.Description);
        cmd.Parameters.AddWithValue("@SpecFile", si.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", si.GUID);
        this.SetLastModified(cmd, si);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateInternalSystemLastModifiedTemp(SInternalSystem si)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_INTERNAL_SYSTEM " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", si.GUID);
        this.SetLastModified(cmd, si);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }


    public void DeleteInternalSystem(SInternalSystem si)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_INTERNAL_SYSTEM WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", si.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region External System

    public List<SExternalSystem> SelectExternalSystemList(bool sortByName, bool onlyMine)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT SE.GUID " +
          "      , SE.ProgramID " +
          "	     , SE.Name " +
          "	     , SE.Description " +
          "      , SE.SpecFile " +
          "      , IFNULL(N.ServiceGUID, 'Y') AS Display " +
          "	     , SE.RegisteredDate,    SE.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    SE.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , SE.LastModifiedDate,  SE.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  SE.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_EXTERNAL_SYSTEM SE " +
          "        INNER JOIN TB_PART P1 ON SE.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON SE.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON SE.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON SE.LastModifiedUserGUID = U2.GUID " +
          "        LEFT OUTER JOIN TB_NO_DISPLAY N ON SE.GUID = N.ServiceGUID ";

        if (onlyMine)
          sql += "  WHERE DISPLAY = 'Y' ";

        if (sortByName)
          sql += " ORDER BY SE.Name ";
        else
          sql += " ORDER BY SE.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      List<SExternalSystem> seList = new List<SExternalSystem>();

      foreach (DataRow row in dt.Rows)
      {
        SExternalSystem se = new SExternalSystem();
        se.GUID = Convert.ToString(row["GUID"]);
        se.ProgramID = Convert.ToString(row["ProgramID"]);
        se.Name = Convert.ToString(row["Name"]);
        se.Description = Convert.ToString(row["Description"]);
        se.SpecFile = Convert.ToString(row["SpecFile"]);
        se.Display = Convert.ToString(row["Display"]);
        
        se.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        se.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        se.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        se.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        se.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        se.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        se.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        se.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        se.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        se.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        seList.Add(se);
      }

      return seList;
    }

    public SExternalSystem SelectExternalSystem(string guid)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT SE.GUID " +
          "      , SE.ProgramID " +
          "	     , SE.Name " +
          "	     , SE.Description " +
          "      , SE.SpecFile " +
          "	     , SE.RegisteredDate,    SE.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    SE.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , SE.LastModifiedDate,  SE.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  SE.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_EXTERNAL_SYSTEM SE " +
          "        INNER JOIN TB_PART P1 ON SE.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON SE.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON SE.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON SE.LastModifiedUserGUID = U2.GUID " +
          "  WHERE SE.GUID = @GUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        adapter.Fill(dt);
      }

      if (dt.Rows.Count == 0)
        return null;

      DataRow row = dt.Rows[0];
      SExternalSystem se = new SExternalSystem();

      se.GUID = Convert.ToString(row["GUID"]);
      se.ProgramID = Convert.ToString(row["ProgramID"]);
      se.Name = Convert.ToString(row["Name"]);
      se.Description = Convert.ToString(row["Description"]);
      se.SpecFile = Convert.ToString(row["SpecFile"]);
    
      se.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
      se.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
      se.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
      se.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
      se.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

      se.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
      se.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
      se.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
      se.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
      se.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

      return se;
    }

    public SExternalSystem InsertExternalSystem()
    {
      SExternalSystem se = new SExternalSystem();
      se.GUID = Guid.NewGuid().ToString();
      se.Name = "New ExternalSystem";
      SCommon.SetDateDesigner(se, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_EXTERNAL_SYSTEM VALUES " +
          " (  @GUID " +
          "  , @ProgramID " +
          "  , @Name " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", se.GUID);
        cmd.Parameters.AddWithValue("@ProgramID", se.ProgramID);
        cmd.Parameters.AddWithValue("@Name", se.Name);
        cmd.Parameters.AddWithValue("@Description", se.Description);
        cmd.Parameters.AddWithValue("@SpecFile", se.SpecFile);
        this.SetRegistered(cmd, se);
        this.SetLastModified(cmd, se);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return se;
    }

    public void UpdateExternalSystem(SExternalSystem se)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_EXTERNAL_SYSTEM " +
          "    SET ProgramID = @ProgramID " +
          "      , Name = @Name " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", se.ProgramID);
        cmd.Parameters.AddWithValue("@Name", se.Name);
        cmd.Parameters.AddWithValue("@Description", se.Description);
        cmd.Parameters.AddWithValue("@SpecFile", se.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", se.GUID);
        this.SetLastModified(cmd, se);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateExternalSystemLastModifiedTemp(SExternalSystem se)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_EXTERNAL_SYSTEM " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", se.GUID);
        this.SetLastModified(cmd, se);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteExternalSystem(SExternalSystem se)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_EXTERNAL_SYSTEM WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", se.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region System

    public DataTable SelectSystemList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT GUID, Type, ProgramID, Name, Description, APICount, PubCount, SubCount, OtherCount " +
          "  FROM( " +
          "       SELECT S.GUID, 'Internal' AS Type, S.ProgramID, S.Name, S.Description " +
          "            , (SELECT COUNT(*) FROM TB_API     WHERE InternalSystemGUID = S.GUID) AS APICount " +
          "            , (SELECT COUNT(*) FROM TB_PUBLISHER WHERE InternalSystemGUID = S.GUID) AS PubCount " +
          "            , (SELECT COUNT(*) FROM TB_SUBSCRIBER  WHERE InternalSystemGUID = S.GUID) AS SubCount " +
          "            , (SELECT COUNT(*) FROM TB_OTHER   WHERE InternalSystemGUID = S.GUID) AS OtherCount " +
          "         FROM TB_INTERNAL_SYSTEM S " +
          "       UNION " +
          "       SELECT S.GUID, 'External' AS Type, S.ProgramID, S.Name, S.Description " +
          "            , (SELECT COUNT(*) FROM TB_API     WHERE ExternalSystemGUID = S.GUID) AS APICount " +
          "            , (SELECT COUNT(*) FROM TB_PUBLISHER WHERE ExternalSystemGUID = S.GUID) AS PubCount " +
          "            , (SELECT COUNT(*) FROM TB_SUBSCRIBER  WHERE ExternalSystemGUID = S.GUID) AS SubCount " +
          "            , (SELECT COUNT(*) FROM TB_OTHER   WHERE ExternalSystemGUID = S.GUID) AS OtherCount " +
          "         FROM TB_EXTERNAL_SYSTEM S " +
          "      ) " +
          " WHERE Name LIKE @Name " +
          " ORDER BY Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    #endregion


    #region Service/System No Display

    public void DeleteNoDisplayListByUser(string userGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_NO_DISPLAY WHERE UserGUID = @UserGUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@UserGUID", userGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void InsertNoDisplay(string serviceGUID, string userGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_NO_DISPLAY VALUES " +
          " (  @ServiceGUID " +
          "  , @UserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ServiceGUID", serviceGUID);
        cmd.Parameters.AddWithValue("@UserGUID", userGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region BizPackage

    public List<SBizPackage> SelectBizPackgeList(string msGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT BP.GUID " +
          "      , BP.MicroserviceGUID, M.Name as MicroserviceName " +
          "      , BP.ProgramID " +
          "      , BP.DesignCompleteDetail " +
          "	     , BP.Name " +
          "	     , BP.NameEnglish " +
          "	     , BP.Description " +
          "	     , BP.SpecFile" +
          "	     , BP.RegisteredDate,    BP.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    BP.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , BP.LastModifiedDate,  BP.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  BP.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_BIZ_PACKAGE BP " +
          "        INNER JOIN TB_MICROSERVICE M ON BP.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_PART P1 ON BP.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON BP.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON BP.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON BP.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(msGUID) == false)
          sql += " WHERE BP.MicroserviceGUID = @MicroserviceGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE BP.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY BP.Name ";
        else
          sql += " ORDER BY BP.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(msGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@MicroserviceGUID", msGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SBizPackage> bpList = new List<SBizPackage>();

      foreach (DataRow row in dt.Rows)
      {
        SBizPackage bp = new SBizPackage();
        bp.GUID = Convert.ToString(row["GUID"]);
        bp.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        bp.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        bp.ProgramID = Convert.ToString(row["ProgramID"]);
        bp.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        bp.Name = Convert.ToString(row["Name"]);
        bp.NameEnglish = Convert.ToString(row["NameEnglish"]);
        bp.Description = Convert.ToString(row["Description"]);
        bp.SpecFile = Convert.ToString(row["SpecFile"]);

        bp.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        bp.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        bp.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        bp.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        bp.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        bp.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        bp.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        bp.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        bp.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        bp.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        bpList.Add(bp);
      }

      return bpList;
    }

    public DataTable SelectBizPackgeList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT BP.GUID " +
          "      , BP.MicroserviceGUID, M.Name as MicroserviceName " +
          "      , BP.ProgramID " +
          "      , BP.DesignCompleteDetail " +
          "	     , BP.Name " +
          "	     , BP.NameEnglish " +
          "	     , BP.Description " +
          "	     , BP.RegisteredDate,    BP.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    BP.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , BP.LastModifiedDate,  BP.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  BP.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "      , (SELECT COUNT(*) FROM TB_API         WHERE BizPackageGUID = BP.GUID) AS APICount " +
          "      , (SELECT COUNT(*) FROM TB_PUBLISHER   WHERE BizPackageGUID = BP.GUID) AS PubCount " +
          "      , (SELECT COUNT(*) FROM TB_SUBSCRIBER  WHERE BizPackageGUID = BP.GUID) AS SubCount " +
          "      , (SELECT COUNT(*) FROM TB_OTHER       WHERE BizPackageGUID = BP.GUID) AS OtherCount " +
          "      , (SELECT COUNT(*) FROM TB_BIZ_RULE_OPERATION    WHERE BizPackageGUID = BP.GUID) AS BROpCount " +
          "      , (SELECT COUNT(*) FROM TB_DATA_ACCESS_OPERATION WHERE BizPackageGUID = BP.GUID) AS DAOpCount " +
          "      , (SELECT COUNT(*) FROM TB_DTO     WHERE BizPackageGUID = BP.GUID) AS DtoCount " +
          "      , (SELECT COUNT(*) FROM TB_ENTITY  WHERE BizPackageGUID = BP.GUID) AS EntityCount " +
          "      , (SELECT COUNT(*) FROM TB_UI      WHERE BizPackageGUID = BP.GUID) AS UICount " +
          "      , (SELECT COUNT(*) FROM TB_JOB     WHERE BizPackageGUID = BP.GUID) AS JobCount " +
          "	  FROM TB_BIZ_PACKAGE BP " +
          "        INNER JOIN TB_MICROSERVICE M ON BP.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_PART P1 ON BP.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON BP.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON BP.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON BP.LastModifiedUserGUID = U2.GUID " +
          " WHERE BP.Name LIKE @Name " +
          " ORDER BY M.Name, BP.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");

        adapter.Fill(dt);
      }

      return dt;
    }

    public SBizPackage InsertBizPackge(SMicroservice ms)
    {
      SBizPackage bp = new SBizPackage();
      bp.MicroserviceGUID = ms.GUID;
      bp.MicroserviceName = ms.Name;
      bp.GUID = Guid.NewGuid().ToString();
      bp.Name = "New BizPackage";
      SCommon.SetDateDesigner(bp, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_BIZ_PACKAGE VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteDetail " + 
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", bp.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", bp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@ProgramID", bp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", bp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", bp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", bp.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", bp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", bp.SpecFile);
        this.SetRegistered(cmd, bp);
        this.SetLastModified(cmd, bp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return bp;
    }

    public void UpdateBizPackge(SBizPackage bp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_PACKAGE " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", bp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", bp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", bp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", bp.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", bp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", bp.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", bp.GUID);
        this.SetLastModified(cmd, bp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizPackgeLastModifiedTemp(SBizPackage bp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_PACKAGE " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", bp.GUID);
        this.SetLastModified(cmd, bp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizPackgeParent(SMicroservice ms, SBizPackage bp)
    {
      bp.MicroserviceGUID = ms.GUID;
      bp.MicroserviceName = ms.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_PACKAGE " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", bp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@GUID", bp.GUID);
        this.SetLastModified(cmd, bp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }


    public void DeleteBizPackge(SBizPackage bp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_BIZ_PACKAGE WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", bp.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Controller

    public List<SController> SelectControllerList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT C.GUID " +
          "      , C.MicroserviceGUID, M.Name AS MicroserviceName " +
          "      , C.BizPackageGUID, BP.Name AS BizPackageName " +
          "      , C.ProgramID " +
          "      , C.DesignCompleteSkeleton " +
          "      , C.DesignCompleteDetail " +
          "	     , C.Name " +
          "	     , C.NameEnglish " +
          "	     , C.URI" +
          "	     , C.Description " +
          "	     , C.SpecFile" +
          "	     , C.RegisteredDate,    C.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    C.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , C.LastModifiedDate,  C.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  C.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_CONTROLLER C " +
          "        INNER JOIN TB_MICROSERVICE M ON C.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE BP ON C.BizPackageGUID = BP.GUID " +
          "        INNER JOIN TB_PART P1 ON C.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON C.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON C.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON C.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE C.BizPackageGUID = @BizPackageGUID ";
        else if(string.IsNullOrEmpty(guid) == false)
          sql += " WHERE C.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY C.Name ";
        else
          sql += " ORDER BY C.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SController> controllerList = new List<SController>();

      foreach (DataRow row in dt.Rows)
      {
        SController controller = new SController();
        controller.GUID = Convert.ToString(row["GUID"]);
        controller.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        controller.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        controller.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        controller.BizPackageName = Convert.ToString(row["BizPackageName"]);
        controller.ProgramID = Convert.ToString(row["ProgramID"]);
        controller.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        controller.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        controller.Name = Convert.ToString(row["Name"]);
        controller.NameEnglish = Convert.ToString(row["NameEnglish"]);
        controller.URI = Convert.ToString(row["URI"]);
        controller.Description = Convert.ToString(row["Description"]);
        controller.SpecFile = Convert.ToString(row["SpecFile"]);
        
        controller.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        controller.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        controller.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        controller.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        controller.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        controller.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        controller.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        controller.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        controller.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        controller.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        controllerList.Add(controller);
      }

      return controllerList;
    }

    public SController InsertController(SBizPackage bp, string name = "")
    {
      SController ctr = new SController();
      ctr.MicroserviceGUID = bp.MicroserviceGUID;
      ctr.MicroserviceName = bp.MicroserviceName;
      ctr.BizPackageGUID = bp.GUID;
      ctr.BizPackageName = bp.Name;
      ctr.GUID = Guid.NewGuid().ToString();
      ctr.Name = "New Controller";
      SCommon.SetDateDesigner(ctr, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_CONTROLLER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @URI " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ctr.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", ctr.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", ctr.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", ctr.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", ctr.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", ctr.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", ctr.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", ctr.NameEnglish);
        cmd.Parameters.AddWithValue("@URI", ctr.URI);
        cmd.Parameters.AddWithValue("@Description", ctr.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ctr.SpecFile);
        this.SetRegistered(cmd, ctr);
        this.SetLastModified(cmd, ctr);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return ctr;
    }

    public void UpdateController(SController ctr)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONTROLLER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , URI = @URI" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", ctr.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", ctr.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", ctr.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", ctr.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", ctr.NameEnglish);
        cmd.Parameters.AddWithValue("@URI", ctr.URI);
        cmd.Parameters.AddWithValue("@Description", ctr.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ctr.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", ctr.GUID);
        this.SetLastModified(cmd, ctr);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateControllerLastModifiedTemp(SController ctr)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONTROLLER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ctr.GUID);
        this.SetLastModified(cmd, ctr);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateControllerParent(SBizPackage bp, SController ctr)
    {
      ctr.MicroserviceGUID = bp.MicroserviceGUID;
      ctr.MicroserviceName = bp.MicroserviceName;
      ctr.BizPackageGUID = bp.GUID;
      ctr.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONTROLLER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", ctr.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", ctr.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", ctr.GUID);
        this.SetLastModified(cmd, ctr);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteController(SController ctr)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_CONTROLLER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ctr.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region API
    
    public List<SAPI> SelectAPIList(SComponent parentComponent, string guid, string dtoGUID, string callBROpGUID, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT A.GUID " +
          "      , A.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , A.InternalSystemGUID, SI.Name AS InternalSystemName " +
          "      , A.ExternalSystemGUID, SE.Name AS ExternalSystemName " +
          "      , A.BizPackageGUID, B.Name AS BizPackageName " +
          "      , A.ControllerGUID, C.Name AS ControllerName " +
          "      , A.ProgramID " +
          "      , A.DesignCompleteSkeleton " +
          "      , A.DesignCompleteDetail " +
          "	     , A.Name " +
          "	     , A.NameEnglish " +
          "      , A.HttpMethod " +
          "      , A.URI " +
          "      , A.InputGUID " +
          "      , A.Input " +
          "      , A.OutputGUID " +
          "      , A.Output " +
          "      , A.CalleeBROperationGUID " +
          "	     , A.Description " +
          "	     , A.SpecFile" +
          "	     , A.RegisteredDate,    A.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    A.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , A.LastModifiedDate,  A.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  A.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_API A " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON A.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON A.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON A.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON A.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_CONTROLLER C ON A.ControllerGUID = C.GUID " +
          "        INNER JOIN TB_PART P1 ON A.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON A.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON A.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON A.LastModifiedUserGUID = U2.GUID ";

        if (parentComponent is SBizPackage)
          sql += "  WHERE A.BizPackageGUID = @BizPackageGUID AND A.ControllerGUID = '' ";        
        else if (parentComponent is SController)        
          sql += "  WHERE A.ControllerGUID = @ControllerGUID ";
        else if (parentComponent is SInternalSystem)        
          sql += "  WHERE A.InternalSystemGUID = @InternalSystemGUID ";
        else if (parentComponent is SExternalSystem)
          sql += "  WHERE A.ExternalSystemGUID = @ExternalSystemGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE A.GUID = @GUID ";
        else if (string.IsNullOrEmpty(dtoGUID) == false)
          sql += " WHERE A.InputGUID = @DtoGUID OR A.OutputGUID = @DtoGUID ";
        else if (string.IsNullOrEmpty(callBROpGUID) == false)
          sql += " WHERE A.CalleeBROperationGUID = @CalleeBROperationGUID ";

        if (sortByName)
          sql += " ORDER BY A.Name ";
        else
          sql += " ORDER BY A.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (parentComponent is SBizPackage)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", parentComponent.GUID);
        else if (parentComponent is SController)
          adapter.SelectCommand.Parameters.AddWithValue("@ControllerGUID", parentComponent.GUID);
        else if (parentComponent is SInternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@InternalSystemGUID", parentComponent.GUID);
        else if (parentComponent is SExternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@ExternalSystemGUID", parentComponent.GUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        else if (string.IsNullOrEmpty(dtoGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@DtoGUID", dtoGUID);
        else if (string.IsNullOrEmpty(callBROpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@CalleeBROperationGUID", callBROpGUID);

        adapter.Fill(dt);
      }

      List<SAPI> apiList = new List<SAPI>();

      foreach (DataRow row in dt.Rows)
      {
        SAPI api = new SAPI();
        api.GUID = Convert.ToString(row["GUID"]);
        api.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        api.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        api.InternalSystemGUID = Convert.ToString(row["InternalSystemGUID"]);
        api.InternalSystemName = Convert.ToString(row["InternalSystemName"]);
        api.ExternalSystemGUID = Convert.ToString(row["ExternalSystemGUID"]);
        api.ExternalSystemName = Convert.ToString(row["ExternalSystemName"]);
        api.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        api.BizPackageName = Convert.ToString(row["BizPackageName"]);
        api.ControllerGUID = Convert.ToString(row["ControllerGUID"]);
        api.ControllerName = Convert.ToString(row["ControllerName"]);
        api.ProgramID = Convert.ToString(row["ProgramID"]);
        api.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        api.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        api.Name = Convert.ToString(row["Name"]);
        api.NameEnglish = Convert.ToString(row["NameEnglish"]);
        api.HttpMethod = Convert.ToString(row["HttpMethod"]);
        api.URI = Convert.ToString(row["URI"]);
        api.InputGUID = Convert.ToString(row["InputGUID"]);
        api.Input = Convert.ToString(row["Input"]);
        api.OutputGUID = Convert.ToString(row["OutputGUID"]);
        api.Output = Convert.ToString(row["Output"]);
        api.CalleeBROperationGUID = Convert.ToString(row["CalleeBROperationGUID"]);
        api.Description = Convert.ToString(row["Description"]);
        api.SpecFile = Convert.ToString(row["SpecFile"]);

        api.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        api.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        api.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        api.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        api.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        api.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        api.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        api.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        api.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        api.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        apiList.Add(api);
      }

      return apiList;
    }

    public DataTable SelectAPIList(string name, string methodName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT A.GUID " +
          "      , IFNULL(M.Name, '') || IFNULL(SI.Name, '') || IFNULL(SE.Name, '') AS ServiceSystem " +
          "      , B.Name AS BizPackageName " +
          "      , C.Name AS ControllerName " +
          "      , A.ProgramID " +
          "      , A.DesignCompleteSkeleton " +
          "      , A.DesignCompleteDetail " +
          "	     , A.Name " +
          "	     , A.NameEnglish " +
          "      , A.HttpMethod " +
          "      , A.URI " +
          "      , A.Input " +
          "      , A.Output " +
          "      , BO.Name AS CalleeBROperationName " +
          "	     , A.Description " +
          "	     , A.RegisteredDate,    A.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    A.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , A.LastModifiedDate,  A.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  A.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_API A " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON A.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON A.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON A.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON A.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_CONTROLLER C ON A.ControllerGUID = C.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_RULE_OPERATION BO ON A.CalleeBROperationGUID = BO.GUID " +
          "        INNER JOIN TB_PART P1 ON A.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON A.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON A.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON A.LastModifiedUserGUID = U2.GUID " +
          "  WHERE A.Name LIKE @Name " + 
          "    AND A.NameEnglish LIKE @NameEnglish " +
          " ORDER BY ServiceSystem, B.Name, C.Name, A.Name ";
        
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.SelectCommand.Parameters.AddWithValue("@NameEnglish", "%" + methodName + "%");

        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertAPI(SAPI api)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_API VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @InternalSystemGUID " +
          "  , @ExternalSystemGUID " +
          "  , @BizPackageGUID " + 
          "  , @ControllerGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @HttpMethod " +
          "  , @URI " +
          "  , @InputGUID " +
          "  , @Input " +
          "  , @OutputGUID " +
          "  , @Output " +
          "  , @CalleeBROperationGUID " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", api.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", api.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", api.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", api.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", api.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ControllerGUID", api.ControllerGUID);
        cmd.Parameters.AddWithValue("@ProgramID", api.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", api.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", api.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", api.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", api.NameEnglish);
        cmd.Parameters.AddWithValue("@HttpMethod", api.HttpMethod);
        cmd.Parameters.AddWithValue("@URI", api.URI);
        cmd.Parameters.AddWithValue("@InputGUID", api.InputGUID);
        cmd.Parameters.AddWithValue("@Input", api.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", api.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", api.Output);
        cmd.Parameters.AddWithValue("@CalleeBROperationGUID", api.CalleeBROperationGUID);
        cmd.Parameters.AddWithValue("@Description", api.Description);
        cmd.Parameters.AddWithValue("@SpecFile", api.SpecFile);
        this.SetRegistered(cmd, api);
        this.SetLastModified(cmd, api);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateAPI(SAPI api)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_API " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , HttpMethod = @HttpMethod " +
          "      , URI = @URI " +
          "      , InputGUID = @InputGUID " +
          "      , Input = @Input " +
          "      , OutputGUID = @OutputGUID " +
          "      , Output = @Output " +
          "      , CalleeBROperationGUID = @CalleeBROperationGUID " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", api.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", api.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", api.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", api.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", api.NameEnglish);
        cmd.Parameters.AddWithValue("@HttpMethod", api.HttpMethod);
        cmd.Parameters.AddWithValue("@URI", api.URI);
        cmd.Parameters.AddWithValue("@InputGUID", api.InputGUID);
        cmd.Parameters.AddWithValue("@Input", api.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", api.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", api.Output);
        cmd.Parameters.AddWithValue("@CalleeBROperationGUID", api.CalleeBROperationGUID);
        cmd.Parameters.AddWithValue("@Description", api.Description);
        cmd.Parameters.AddWithValue("@SpecFile", api.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", api.GUID);
        this.SetLastModified(cmd, api);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateAPILastModifiedTemp(SAPI api)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_API " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", api.GUID);
        this.SetLastModified(cmd, api);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateAPIParent(SComponent parentComponent, SAPI api)
    {
      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        api.MicroserviceGUID = bp.MicroserviceGUID;
        api.MicroserviceName = bp.MicroserviceName;
        api.BizPackageGUID = bp.GUID;
        api.BizPackageName = bp.Name;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SController)
      {
        SController controller = parentComponent as SController;
        api.MicroserviceGUID = controller.MicroserviceGUID;
        api.MicroserviceName = controller.MicroserviceName;
        api.BizPackageGUID = controller.BizPackageGUID;
        api.BizPackageName = controller.BizPackageName;
        api.ControllerGUID = controller.GUID;
        api.ControllerName = controller.Name;
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        api.MicroserviceGUID = string.Empty;
        api.MicroserviceName = string.Empty;
        api.BizPackageGUID = string.Empty;
        api.BizPackageName = string.Empty;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = si.GUID;
        api.InternalSystemName = si.Name;
        api.ExternalSystemGUID = string.Empty;
        api.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        api.MicroserviceGUID = string.Empty;
        api.MicroserviceName = string.Empty;
        api.BizPackageGUID = string.Empty;
        api.BizPackageName = string.Empty;
        api.ControllerGUID = string.Empty;
        api.ControllerName = string.Empty;
        api.InternalSystemGUID = string.Empty;
        api.InternalSystemName = string.Empty;
        api.ExternalSystemGUID = se.GUID;
        api.ExternalSystemName = se.Name;
      }

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_API " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , InternalSystemGUID = @InternalSystemGUID " +
          "      , ExternalSystemGUID = @ExternalSystemGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , ControllerGUID = @ControllerGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", api.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", api.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", api.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", api.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ControllerGUID", api.ControllerGUID);
        cmd.Parameters.AddWithValue("@GUID", api.GUID);
        this.SetLastModified(cmd, api);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteAPI(SAPI api)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_API WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", api.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Producer

    public List<SProducer> SelectProducerList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT P.GUID " +
          "      , P.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , P.BizPackageGUID, B.Name AS BizPackageName " +
          "      , P.ProgramID " +
          "      , P.DesignCompleteSkeleton " +
          "      , P.DesignCompleteDetail " +
          "	     , P.Name " +
          "	     , P.NameEnglish " +
          "	     , P.Description " +
          "	     , P.SpecFile" +
          "	     , P.RegisteredDate,    P.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    P.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , P.LastModifiedDate,  P.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  P.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_PRODUCER P " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON P.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON P.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON P.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON P.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON P.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON P.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE P.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE P.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY P.Name ";
        else
          sql += " ORDER BY P.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SProducer> producerList = new List<SProducer>();

      foreach (DataRow row in dt.Rows)
      {
        SProducer producer = new SProducer();
        producer.GUID = Convert.ToString(row["GUID"]);
        producer.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        producer.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        producer.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        producer.BizPackageName = Convert.ToString(row["BizPackageName"]);
        producer.ProgramID = Convert.ToString(row["ProgramID"]);
        producer.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        producer.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        producer.Name = Convert.ToString(row["Name"]);
        producer.NameEnglish = Convert.ToString(row["NameEnglish"]);
        producer.Description = Convert.ToString(row["Description"]);
        producer.SpecFile = Convert.ToString(row["SpecFile"]);
        
        producer.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        producer.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        producer.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        producer.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        producer.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        producer.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        producer.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        producer.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        producer.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        producer.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        producerList.Add(producer);
      }

      return producerList;
    }

    public SProducer InsertProducer(SBizPackage bp, string name = "")
    {
      SProducer producer = new SProducer();
      producer.GUID = Guid.NewGuid().ToString();
      producer.MicroserviceGUID = bp.MicroserviceGUID;
      producer.MicroserviceName = bp.MicroserviceName;
      producer.BizPackageGUID = bp.GUID;
      producer.BizPackageName = bp.Name;
      producer.Name = "New Producer";
      SCommon.SetDateDesigner(producer, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_PRODUCER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", producer.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", producer.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", producer.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", producer.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", producer.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", producer.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", producer.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", producer.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", producer.Description);
        cmd.Parameters.AddWithValue("@SpecFile", producer.SpecFile);
        this.SetRegistered(cmd, producer);
        this.SetLastModified(cmd, producer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return producer;
    }

    public void UpdateProducer(SProducer producer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PRODUCER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", producer.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", producer.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", producer.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", producer.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", producer.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", producer.Description);
        cmd.Parameters.AddWithValue("@SpecFile", producer.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", producer.GUID);
        this.SetLastModified(cmd, producer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateProducerLastModifiedTemp(SProducer producer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PRODUCER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", producer.GUID);
        this.SetLastModified(cmd, producer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateProducerParent(SBizPackage bp, SProducer producer)
    {
      producer.MicroserviceGUID = bp.MicroserviceGUID;
      producer.MicroserviceName = bp.MicroserviceName;
      producer.BizPackageGUID = bp.GUID;
      producer.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PRODUCER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", producer.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", producer.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", producer.GUID);
        this.SetLastModified(cmd, producer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteProducer(SProducer producer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_PRODUCER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", producer.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Publisher

    public List<SPublisher> SelectPublisherList(SComponent parentComponent, string guid, string inputGUID, string topic, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT P.GUID " +
          "      , P.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , P.InternalSystemGUID, SI.Name AS InternalSystemName " +
          "      , P.ExternalSystemGUID, SE.Name AS ExternalSystemName " +
          "      , P.BizPackageGUID, B.Name AS BizPackageName " +
          "      , P.ProducerGUID, PD.Name AS ProducerName " +
          "      , P.ProgramID " +
          "      , P.DesignCompleteSkeleton " +
          "      , P.DesignCompleteDetail " +
          "	     , P.Name " +
          "	     , P.NameEnglish " +
          "      , P.InputGUID " +
          "      , P.Input " +
          "      , P.Topic " +
          "	     , P.Description " +
          "	     , P.SpecFile" +
          "	     , P.RegisteredDate,    P.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    P.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , P.LastModifiedDate,  P.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  P.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_PUBLISHER P " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON P.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON P.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON P.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON P.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_PRODUCER PD ON P.ProducerGUID = PD.GUID " +
          "        INNER JOIN TB_PART P1 ON P.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON P.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON P.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON P.LastModifiedUserGUID = U2.GUID ";

        if (parentComponent is SBizPackage)
          sql += " WHERE P.BizPackageGUID = @BizPackageGUID AND P.ProducerGUID = '' ";
        else if (parentComponent is SProducer)
          sql += " WHERE P.ProducerGUID = @ProducerGUID ";
        else if (parentComponent is SInternalSystem)
          sql += " WHERE P.InternalSystemGUID = @InternalSystemGUID ";
        else if (parentComponent is SExternalSystem)
          sql += " WHERE P.ExternalSystemGUID = @ExternalSystemGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE P.GUID = @GUID ";
        else if (string.IsNullOrEmpty(inputGUID) == false)
          sql += " WHERE P.InputGUID = @InputGUID ";
        else if (string.IsNullOrEmpty(topic) == false)
          sql += " WHERE P.Topic = @Topic ";

        if (sortByName)
          sql += " ORDER BY P.Name ";
        else
          sql += " ORDER BY P.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (parentComponent is SBizPackage)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", parentComponent.GUID);
        else if (parentComponent is SProducer)
          adapter.SelectCommand.Parameters.AddWithValue("@ProducerGUID", parentComponent.GUID);
        else if (parentComponent is SInternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@InternalSystemGUID", parentComponent.GUID);
        else if (parentComponent is SExternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@ExternalSystemGUID", parentComponent.GUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        else if (string.IsNullOrEmpty(inputGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@InputGUID", inputGUID);
        else if (string.IsNullOrEmpty(topic) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@Topic", topic);

        adapter.Fill(dt);
      }

      List<SPublisher> pubList = new List<SPublisher>();

      foreach (DataRow row in dt.Rows)
      {
        SPublisher pub = new SPublisher();
        pub.GUID = Convert.ToString(row["GUID"]);
        pub.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        pub.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        pub.InternalSystemGUID = Convert.ToString(row["InternalSystemGUID"]);
        pub.InternalSystemName = Convert.ToString(row["InternalSystemName"]);
        pub.ExternalSystemGUID = Convert.ToString(row["ExternalSystemGUID"]);
        pub.ExternalSystemName = Convert.ToString(row["ExternalSystemName"]);
        pub.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        pub.BizPackageName = Convert.ToString(row["BizPackageName"]);
        pub.ProducerGUID = Convert.ToString(row["ProducerGUID"]);
        pub.ProducerName = Convert.ToString(row["ProducerName"]);
        pub.ProgramID = Convert.ToString(row["ProgramID"]);
        pub.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        pub.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        pub.Name = Convert.ToString(row["Name"]);
        pub.NameEnglish = Convert.ToString(row["NameEnglish"]);
        pub.InputGUID = Convert.ToString(row["InputGUID"]);
        pub.Input = Convert.ToString(row["Input"]);
        pub.Topic = Convert.ToString(row["Topic"]);
        pub.Description = Convert.ToString(row["Description"]);
        pub.SpecFile = Convert.ToString(row["SpecFile"]);
        
        pub.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        pub.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        pub.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        pub.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        pub.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        pub.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        pub.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        pub.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        pub.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        pub.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        pubList.Add(pub);
      }

      return pubList;
    }

    public DataTable SelectPublisherList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT P.GUID " +
          "      , IFNULL(M.Name, '') || IFNULL(SI.Name, '') || IFNULL(SE.Name, '') AS ServiceSystem " +
          "      , B.Name AS BizPackageName " +
          "      , PD.Name AS ProducerName " +
          "      , P.ProgramID " +
          "      , P.DesignCompleteSkeleton " +
          "      , P.DesignCompleteDetail " +
          "	     , P.Name " +
          "	     , P.NameEnglish " +
          "      , P.Input " +
          "      , P.Topic " +
          "	     , P.Description " +
          "	     , P.RegisteredDate,    P.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    P.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , P.LastModifiedDate,  P.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  P.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_PUBLISHER P " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON P.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON P.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON P.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON P.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_PRODUCER PD ON P.ProducerGUID = PD.GUID " +
          "        INNER JOIN TB_PART P1 ON P.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON P.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON P.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON P.LastModifiedUserGUID = U2.GUID " +
          " WHERE P.Name LIKE @Name " +
          " ORDER BY ServiceSystem, B.Name, PD.Name, P.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertPublisher(SPublisher pub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_PUBLISHER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @InternalSystemGUID " +
          "  , @ExternalSystemGUID " +
          "  , @BizPackageGUID " +
          "  , @ProducerGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @InputGUID " +
          "  , @Input " +
          "  , @Topic " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", pub.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", pub.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", pub.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", pub.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", pub.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProducerGUID", pub.ProducerGUID);
        cmd.Parameters.AddWithValue("@ProgramID", pub.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", pub.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", pub.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", pub.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", pub.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", pub.InputGUID);
        cmd.Parameters.AddWithValue("@Input", pub.Input);
        cmd.Parameters.AddWithValue("@Topic", pub.Topic);
        cmd.Parameters.AddWithValue("@Description", pub.Description);
        cmd.Parameters.AddWithValue("@SpecFile", pub.SpecFile);
        this.SetRegistered(cmd, pub);
        this.SetLastModified(cmd, pub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdatePublisher(SPublisher pub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PUBLISHER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , InputGUID = @InputGUID " +
          "      , Input = @Input " +
          "      , Topic = @Topic " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", pub.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", pub.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", pub.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", pub.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", pub.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", pub.InputGUID);
        cmd.Parameters.AddWithValue("@Input", pub.Input);
        cmd.Parameters.AddWithValue("@Topic", pub.Topic);
        cmd.Parameters.AddWithValue("@Description", pub.Description);
        cmd.Parameters.AddWithValue("@SpecFile", pub.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", pub.GUID);
        this.SetLastModified(cmd, pub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdatePublisherLastModifiedTemp(SPublisher pub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PUBLISHER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", pub.GUID);
        this.SetLastModified(cmd, pub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdatePublisherParent(SComponent parentComponent, SPublisher pub)
    {
      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        pub.MicroserviceGUID = bp.MicroserviceGUID;
        pub.MicroserviceName = bp.MicroserviceName;
        pub.BizPackageGUID = bp.GUID;
        pub.BizPackageName = bp.Name;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SProducer)
      {
        SProducer producer = parentComponent as SProducer;
        pub.MicroserviceGUID = producer.MicroserviceGUID;
        pub.MicroserviceName = producer.MicroserviceName;
        pub.BizPackageGUID = producer.BizPackageGUID;
        pub.BizPackageName = producer.BizPackageName;
        pub.ProducerGUID = producer.GUID;
        pub.ProducerName = producer.Name;
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        pub.MicroserviceGUID = string.Empty;
        pub.MicroserviceName = string.Empty;
        pub.BizPackageGUID = string.Empty;
        pub.BizPackageName = string.Empty;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = si.GUID;
        pub.InternalSystemName = si.Name;
        pub.ExternalSystemGUID = string.Empty;
        pub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        pub.MicroserviceGUID = string.Empty;
        pub.MicroserviceName = string.Empty;
        pub.BizPackageGUID = string.Empty;
        pub.BizPackageName = string.Empty;
        pub.ProducerGUID = string.Empty;
        pub.ProducerName = string.Empty;
        pub.InternalSystemGUID = string.Empty;
        pub.InternalSystemName = string.Empty;
        pub.ExternalSystemGUID = se.GUID;
        pub.ExternalSystemName = se.GUID;
      }

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_PUBLISHER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , InternalSystemGUID = @InternalSystemGUID " +
          "      , ExternalSystemGUID = @ExternalSystemGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , ProducerGUID = @ProducerGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", pub.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", pub.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", pub.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", pub.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProducerGUID", pub.ProducerGUID);
        cmd.Parameters.AddWithValue("@GUID", pub.GUID);
        this.SetLastModified(cmd, pub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeletePublisher(SPublisher pub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_PUBLISHER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", pub.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Consumer

    public List<SConsumer> SelectConsumerList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT C.GUID " +
          "      , C.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , C.BizPackageGUID, B.Name AS BizPackageName " +
          "      , C.ProgramID " +
          "      , C.DesignCompleteSkeleton " +
          "      , C.DesignCompleteDetail " +
          "	     , C.Name " +
          "	     , C.NameEnglish " +
          "	     , C.Description " +
          "	     , C.SpecFile" +
          "	     , C.RegisteredDate,    C.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    C.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , C.LastModifiedDate,  C.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  C.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_CONSUMER C " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON C.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON C.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON C.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON C.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON C.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON C.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE C.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE C.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY C.Name ";
        else
          sql += " ORDER BY C.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SConsumer> consumerList = new List<SConsumer>();

      foreach (DataRow row in dt.Rows)
      {
        SConsumer consumer = new SConsumer();
        consumer.GUID = Convert.ToString(row["GUID"]);
        consumer.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        consumer.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        consumer.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        consumer.BizPackageName = Convert.ToString(row["BizPackageName"]);
        consumer.ProgramID = Convert.ToString(row["ProgramID"]);
        consumer.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        consumer.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        consumer.Name = Convert.ToString(row["Name"]);
        consumer.NameEnglish = Convert.ToString(row["NameEnglish"]);
        consumer.Description = Convert.ToString(row["Description"]);
        consumer.SpecFile = Convert.ToString(row["SpecFile"]);
        
        consumer.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        consumer.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        consumer.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        consumer.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        consumer.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        consumer.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        consumer.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        consumer.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        consumer.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        consumer.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        consumerList.Add(consumer);
      }

      return consumerList;
    }

    public SConsumer InsertConsumer(SBizPackage bp, string name = "")
    {
      SConsumer consumer = new SConsumer();
      consumer.GUID = Guid.NewGuid().ToString();
      consumer.MicroserviceGUID = bp.MicroserviceGUID;
      consumer.MicroserviceName = bp.MicroserviceName;
      consumer.BizPackageGUID = bp.GUID;
      consumer.BizPackageName = bp.Name;
      consumer.Name = "New Consumer";
      SCommon.SetDateDesigner(consumer, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_CONSUMER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", consumer.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", consumer.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", consumer.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", consumer.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", consumer.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", consumer.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", consumer.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", consumer.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", consumer.Description);
        cmd.Parameters.AddWithValue("@SpecFile", consumer.SpecFile);
        this.SetRegistered(cmd, consumer);
        this.SetLastModified(cmd, consumer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return consumer;
    }

    public void UpdateConsumer(SConsumer consumer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONSUMER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", consumer.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", consumer.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", consumer.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", consumer.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", consumer.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", consumer.Description);
        cmd.Parameters.AddWithValue("@SpecFile", consumer.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", consumer.GUID);
        this.SetLastModified(cmd, consumer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateConsumerLastModifiedTemp(SConsumer consumer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONSUMER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", consumer.GUID);
        this.SetLastModified(cmd, consumer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateConsumerParent(SBizPackage bp, SConsumer consumer)
    {
      consumer.MicroserviceGUID = bp.MicroserviceGUID;
      consumer.MicroserviceName = bp.MicroserviceName;
      consumer.BizPackageGUID = bp.GUID;
      consumer.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_CONSUMER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", consumer.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", consumer.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", consumer.GUID);
        this.SetLastModified(cmd, consumer);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteConsumer(SConsumer consumer)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_CONSUMER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", consumer.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Subscriber

    public List<SSubscriber> SelectSubscriberList(SComponent parentComponent, string guid, string inputGUID, string calleeBROpGUID, string topic, bool sortByName = true)
    {
      DataTable dt = new DataTable();
    
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT S.GUID " +
          "      , S.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , S.InternalSystemGUID, SI.Name AS InternalSystemName " +
          "      , S.ExternalSystemGUID, SE.Name AS ExternalSystemName " +
          "      , S.BizPackageGUID, B.Name AS BizPackageName " +
          "      , S.ConsumerGUID, CS.Name AS ConsumerName " +
          "      , S.ProgramID " +
          "      , S.DesignCompleteSkeleton " +
          "      , S.DesignCompleteDetail " +
          "	     , S.Name " +
          "	     , S.NameEnglish " +
          "      , S.InputGUID " +
          "      , S.Input " +
          "      , S.Topic " +
          "      , S.CalleeBROperationGUID " + 
          "	     , S.Description " +
          "	     , S.SpecFile" +
          "	     , S.RegisteredDate,    S.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    S.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , S.LastModifiedDate,  S.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  S.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_SUBSCRIBER S " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON S.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON S.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON S.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON S.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_CONSUMER CS ON S.ConsumerGUID = CS.GUID " +
          "        INNER JOIN TB_PART P1 ON S.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON S.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON S.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON S.LastModifiedUserGUID = U2.GUID ";

        if (parentComponent is SBizPackage)
          sql += "  WHERE S.BizPackageGUID = @BizPackageGUID AND S.ConsumerGUID = '' ";
        else if (parentComponent is SConsumer)
          sql += "  WHERE S.ConsumerGUID = @ConsumerGUID ";
        else if (parentComponent is SInternalSystem)
          sql += "  WHERE S.InternalSystemGUID = @InternalSystemGUID ";
        else if (parentComponent is SExternalSystem)
          sql += "  WHERE S.ExternalSystemGUID = @ExternalSystemGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += "  WHERE S.GUID = @GUID ";
        else if (string.IsNullOrEmpty(inputGUID) == false)
          sql += "  WHERE S.InputGUID = @InputGUID ";
        else if (string.IsNullOrEmpty(calleeBROpGUID) == false)
          sql += "  WHERE S.CalleeBROperationGUID = @CalleeBROperationGUID ";
        else if (string.IsNullOrEmpty(topic) == false)
          sql += "  WHERE S.Topic = @Topic ";

        if (sortByName)
          sql += " ORDER BY S.Name ";
        else
          sql += " ORDER BY S.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (parentComponent is SBizPackage)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", parentComponent.GUID);
        else if (parentComponent is SConsumer)
          adapter.SelectCommand.Parameters.AddWithValue("@ConsumerGUID", parentComponent.GUID);
        else if (parentComponent is SInternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@InternalSystemGUID", parentComponent.GUID);
        else if (parentComponent is SExternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@ExternalSystemGUID", parentComponent.GUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        else if (string.IsNullOrEmpty(inputGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@InputGUID", inputGUID);
        else if (string.IsNullOrEmpty(calleeBROpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@CalleeBROperationGUID", calleeBROpGUID);
        else if (string.IsNullOrEmpty(topic) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@Topic", topic);

        adapter.Fill(dt);
      }

      List<SSubscriber> subList = new List<SSubscriber>();

      foreach (DataRow row in dt.Rows)
      {
        SSubscriber sub = new SSubscriber();
        sub.GUID = Convert.ToString(row["GUID"]);
        sub.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        sub.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        sub.InternalSystemGUID = Convert.ToString(row["InternalSystemGUID"]);
        sub.InternalSystemName = Convert.ToString(row["InternalSystemName"]);
        sub.ExternalSystemGUID = Convert.ToString(row["ExternalSystemGUID"]);
        sub.ExternalSystemName = Convert.ToString(row["ExternalSystemName"]);
        sub.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        sub.BizPackageName = Convert.ToString(row["BizPackageName"]);
        sub.ConsumerGUID = Convert.ToString(row["ConsumerGUID"]);
        sub.ConsumerName = Convert.ToString(row["ConsumerName"]);
        sub.ProgramID = Convert.ToString(row["ProgramID"]);
        sub.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        sub.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        sub.Name = Convert.ToString(row["Name"]);
        sub.NameEnglish = Convert.ToString(row["NameEnglish"]);
        sub.InputGUID = Convert.ToString(row["InputGUID"]);
        sub.Input = Convert.ToString(row["Input"]);
        sub.Topic = Convert.ToString(row["Topic"]);
        sub.CalleeBROperationGUID = Convert.ToString(row["CalleeBROperationGUID"]);
        sub.Description = Convert.ToString(row["Description"]);
        sub.SpecFile = Convert.ToString(row["SpecFile"]);
        
        sub.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        sub.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        sub.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        sub.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        sub.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        sub.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        sub.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        sub.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        sub.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        sub.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        subList.Add(sub);
      }

      return subList;
    }

    public DataTable SelectSubscriberList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT S.GUID " +
          "      , IFNULL(M.Name, '') || IFNULL(SI.Name, '') || IFNULL(SE.Name, '') AS ServiceSystem " +
          "      , B.Name AS BizPackageName " +
          "      , CS.Name AS ConsumerName " +
          "      , S.ProgramID " +
          "      , S.DesignCompleteSkeleton " +
          "      , S.DesignCompleteDetail " +
          "	     , S.Name " +
          "	     , S.NameEnglish " +
          "      , S.Input " +
          "      , S.Topic " +
          "      , BO.Name AS CalleeBROperationName " +
          "	     , S.Description " +
          "	     , S.RegisteredDate,    S.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    S.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , S.LastModifiedDate,  S.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  S.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_SUBSCRIBER S " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON S.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON S.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON S.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON S.BizPackageGUID = B.GUID " +
          "        LEFT OUTER JOIN TB_CONSUMER CS ON S.ConsumerGUID = CS.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_RULE_OPERATION BO ON S.CalleeBROperationGUID = BO.GUID " +
          "        INNER JOIN TB_PART P1 ON S.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON S.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON S.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON S.LastModifiedUserGUID = U2.GUID " +
          "  WHERE S.Name LIKE @Name " +
          " ORDER BY ServiceSystem, B.Name, CS.Name, S.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertSubscriber(SSubscriber sub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_SUBSCRIBER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @InternalSystemGUID " +
          "  , @ExternalSystemGUID " +
          "  , @BizPackageGUID " +
          "  , @ConsumerGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @InputGUID " +
          "  , @Input " +
          "  , @Topic " +
          "  , @CalleeBROperationGUID " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", sub.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", sub.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", sub.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", sub.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", sub.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ConsumerGUID", sub.ConsumerGUID);
        cmd.Parameters.AddWithValue("@ProgramID", sub.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", sub.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", sub.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", sub.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", sub.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", sub.InputGUID);
        cmd.Parameters.AddWithValue("@Input", sub.Input);
        cmd.Parameters.AddWithValue("@Topic", sub.Topic);
        cmd.Parameters.AddWithValue("@CalleeBROperationGUID", sub.CalleeBROperationGUID);
        cmd.Parameters.AddWithValue("@Description", sub.Description);
        cmd.Parameters.AddWithValue("@SpecFile", sub.SpecFile);
        this.SetRegistered(cmd, sub);
        this.SetLastModified(cmd, sub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateSubscriber(SSubscriber sub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_SUBSCRIBER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , InputGUID = @InputGUID " +
          "      , Input = @Input " +
          "      , Topic = @Topic " +
          "      , CalleeBROperationGUID = @CalleeBROperationGUID " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", sub.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", sub.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", sub.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", sub.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", sub.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", sub.InputGUID);
        cmd.Parameters.AddWithValue("@Input", sub.Input);
        cmd.Parameters.AddWithValue("@Topic", sub.Topic);
        cmd.Parameters.AddWithValue("@CalleeBROperationGUID", sub.CalleeBROperationGUID);
        cmd.Parameters.AddWithValue("@Description", sub.Description);
        cmd.Parameters.AddWithValue("@SpecFile", sub.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", sub.GUID);
        this.SetLastModified(cmd, sub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateSubscriberLastModifiedTemp(SSubscriber sub)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_SUBSCRIBER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", sub.GUID);
        this.SetLastModified(cmd, sub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateSubscriberParent(SComponent parentComponent, SSubscriber sub)
    {
      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        sub.MicroserviceGUID = bp.MicroserviceGUID;
        sub.MicroserviceName = bp.MicroserviceName;
        sub.BizPackageGUID = bp.GUID;
        sub.BizPackageName = bp.Name;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SConsumer)
      {
        SConsumer consumer = parentComponent as SConsumer;
        sub.MicroserviceGUID = consumer.MicroserviceGUID;
        sub.MicroserviceName = consumer.MicroserviceName;
        sub.BizPackageGUID = consumer.BizPackageGUID;
        sub.BizPackageName = consumer.BizPackageName;
        sub.ConsumerGUID = consumer.GUID;
        sub.ConsumerName = consumer.Name;
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        sub.MicroserviceGUID = string.Empty;
        sub.MicroserviceName = string.Empty;
        sub.BizPackageGUID = string.Empty;
        sub.BizPackageName = string.Empty;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = si.GUID;
        sub.InternalSystemName = si.Name;
        sub.ExternalSystemGUID = string.Empty;
        sub.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        sub.MicroserviceGUID = string.Empty;
        sub.MicroserviceName = string.Empty;
        sub.BizPackageGUID = string.Empty;
        sub.BizPackageName = string.Empty;
        sub.ConsumerGUID = string.Empty;
        sub.ConsumerName = string.Empty;
        sub.InternalSystemGUID = string.Empty;
        sub.InternalSystemName = string.Empty;
        sub.ExternalSystemGUID = se.GUID;
        sub.ExternalSystemName = se.Name;
      }

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_SUBSCRIBER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , InternalSystemGUID = @InternalSystemGUID " +
          "      , ExternalSystemGUID = @ExternalSystemGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , ConsumerGUID = @ConsumerGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", sub.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", sub.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", sub.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", sub.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ConsumerGUID", sub.ConsumerGUID);
        cmd.Parameters.AddWithValue("@GUID", sub.GUID);
        this.SetLastModified(cmd, sub);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteSubscriber(SSubscriber sub)
    { 
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_SUBSCRIBER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", sub.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Topic

    public List<string> SelectTopicList()
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT DISTINCT(Topic) AS Topic " +
          "   FROM ( " +
          "         SELECT P.Topic FROM TB_PUBLISHER P " +
          "         UNION " +
          "         SELECT S.Topic FROM TB_SUBSCRIBER S " +
          "        ) " +
          "  WHERE Topic != '' " +
          " ORDER BY Topic";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      List<string> topicList = new List<string>();

      foreach (DataRow row in dt.Rows)
        topicList.Add(Convert.ToString(row["Topic"]));

      return topicList;
    }

    #endregion


    #region Other

    public List<SOther> SelectOtherList(SComponent parentComponent, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , O.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , O.InternalSystemGUID, SI.Name AS InternalSystemName " +
          "      , O.ExternalSystemGUID, SE.Name AS ExternalSystemName " +
          "      , O.BizPackageGUID, B.Name AS BizPackageName " +
          "      , O.ProgramID " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.SenderReceiver " +
          "      , O.SystemType " +
          "      , O.SystemGUID " +
          "      , S.Name AS SystemName " +
          "      , O.Type " +
          "      , O.TypeText " +
          "      , O.Input " +
          "      , O.Output " +
          "	     , O.Description " +
          "	     , O.SpecFile" +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_OTHER O " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON O.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON O.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON O.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON O.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID " +
          "        LEFT OUTER JOIN (SELECT GUID, Name from TB_INTERNAL_SYSTEM UNION SELECT GUID, Name from TB_EXTERNAL_SYSTEM) S on O.SystemGUID = S.GUID ";

        if (parentComponent is SBizPackage)
          sql += "  WHERE O.BizPackageGUID = @BizPackageGUID ";
        else if (parentComponent is SInternalSystem)
          sql += "  WHERE O.InternalSystemGUID = @InternalSystemGUID ";
        else if (parentComponent is SExternalSystem)
          sql += "  WHERE O.ExternalSystemGUID = @ExternalSystemGUID ";
        else if(string.IsNullOrEmpty(guid) == false)
          sql += "  WHERE O.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY O.Name ";
        else
          sql += " ORDER BY O.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (parentComponent is SBizPackage)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", parentComponent.GUID);
        else if (parentComponent is SInternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@InternalSystemGUID", parentComponent.GUID);
        else if (parentComponent is SExternalSystem)
          adapter.SelectCommand.Parameters.AddWithValue("@ExternalSystemGUID", parentComponent.GUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SOther> otherList = new List<SOther>();

      foreach (DataRow row in dt.Rows)
      {
        SOther other = new SOther();
        other.GUID = Convert.ToString(row["GUID"]);
        other.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        other.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        other.InternalSystemGUID = Convert.ToString(row["InternalSystemGUID"]);
        other.InternalSystemName = Convert.ToString(row["InternalSystemName"]);
        other.ExternalSystemGUID = Convert.ToString(row["ExternalSystemGUID"]);
        other.ExternalSystemName = Convert.ToString(row["ExternalSystemName"]);
        other.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        other.BizPackageName = Convert.ToString(row["BizPackageName"]);
        other.ProgramID = Convert.ToString(row["ProgramID"]);
        other.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        other.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        other.Name = Convert.ToString(row["Name"]);
        other.NameEnglish = Convert.ToString(row["NameEnglish"]);
        other.SenderReceiver = Convert.ToString(row["SenderReceiver"]);

        if (SComponentType.InternalSystem.ToString() == Convert.ToString(row["SystemType"]))
          other.SystemType = SComponentType.InternalSystem;
        else if (SComponentType.ExternalSystem.ToString() == Convert.ToString(row["SystemType"]))
          other.SystemType = SComponentType.ExternalSystem;

        other.SystemGUID = Convert.ToString(row["SystemGUID"]);
        other.SystemName = Convert.ToString(row["SystemName"]);
        other.Type = Convert.ToString(row["Type"]);
        other.TypeText = Convert.ToString(row["TypeText"]);
        other.Input = Convert.ToString(row["Input"]);
        other.Output = Convert.ToString(row["Output"]);
        other.Description = Convert.ToString(row["Description"]);
        other.SpecFile = Convert.ToString(row["SpecFile"]);
        
        other.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        other.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        other.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        other.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        other.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        other.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        other.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        other.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        other.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        other.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        otherList.Add(other);
      }

      return otherList;
    }

    public DataTable SelectOtherList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , IFNULL(M.Name, '') || IFNULL(SI.Name, '') || IFNULL(SE.Name, '') AS ServiceSystem " +
          "      , B.Name AS BizPackageName " +
          "      , O.ProgramID " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.SenderReceiver " +
          "      , O.SystemType " +
          "      , S.Name AS SystemName " +
          "      , O.Type " +
          "      , O.TypeText " +
          "      , O.Input " +
          "      , O.Output " +
          "	     , O.Description " +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_OTHER O " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON O.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_INTERNAL_SYSTEM SI ON O.InternalSystemGUID = SI.GUID " +
          "        LEFT OUTER JOIN TB_EXTERNAL_SYSTEM SE ON O.ExternalSystemGUID = SE.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON O.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID " +
          "        LEFT OUTER JOIN (SELECT GUID, Name from TB_INTERNAL_SYSTEM UNION SELECT GUID, Name from TB_EXTERNAL_SYSTEM) S on O.SystemGUID = S.GUID " +
          "  WHERE O.Name LIKE @Name " +
          " ORDER BY ServiceSystem, B.Name, O.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertOther(SOther other)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_OTHER VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @InternalSystemGUID " +
          "  , @ExternalSystemGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @SenderReceiver " +
          "  , @SystemType " +
          "  , @SystemGUID " +
          "  , @Type " +
          "  , @TypeText " +
          "  , @Input " +
          "  , @Output " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", other.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", other.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", other.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", other.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", other.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", other.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", other.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", other.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", other.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", other.NameEnglish);
        cmd.Parameters.AddWithValue("@SenderReceiver", other.SenderReceiver);
        cmd.Parameters.AddWithValue("@SystemType", other.SystemType.ToString());
        cmd.Parameters.AddWithValue("@SystemGUID", other.SystemGUID);
        cmd.Parameters.AddWithValue("@Type", other.Type);
        cmd.Parameters.AddWithValue("@TypeText", other.TypeText);
        cmd.Parameters.AddWithValue("@Input", other.Input);
        cmd.Parameters.AddWithValue("@Output", other.Output);
        cmd.Parameters.AddWithValue("@Description", other.Description);
        cmd.Parameters.AddWithValue("@SpecFile", other.SpecFile);
        this.SetRegistered(cmd, other);
        this.SetLastModified(cmd, other);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateOther(SOther other)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_OTHER " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , SenderReceiver = @SenderReceiver " +
          "      , SystemType = @SystemType " +
          "      , SystemGUID = @SystemGUID " +
          "      , Type = @Type " +
          "      , TypeText = @TypeText " +
          "      , Input = @Input " +
          "      , Output = @Output " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", other.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", other.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", other.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", other.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", other.NameEnglish);
        cmd.Parameters.AddWithValue("@SenderReceiver", other.SenderReceiver);
        cmd.Parameters.AddWithValue("@SystemType", other.SystemType.ToString());
        cmd.Parameters.AddWithValue("@SystemGUID", other.SystemGUID);
        cmd.Parameters.AddWithValue("@Type", other.Type);
        cmd.Parameters.AddWithValue("@TypeText", other.TypeText);
        cmd.Parameters.AddWithValue("@Input", other.Input);
        cmd.Parameters.AddWithValue("@Output", other.Output);
        cmd.Parameters.AddWithValue("@Description", other.Description);
        cmd.Parameters.AddWithValue("@SpecFile", other.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", other.GUID);
        this.SetLastModified(cmd, other);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateOtherLastModifiedTemp(SOther other)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_OTHER " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", other.GUID);
        this.SetLastModified(cmd, other);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateOtherParent(SComponent parentComponent, SOther other)
    {
      if (parentComponent is SBizPackage)
      {
        SBizPackage bp = parentComponent as SBizPackage;
        other.MicroserviceGUID = bp.MicroserviceGUID;
        other.MicroserviceName = bp.MicroserviceName;
        other.BizPackageGUID = bp.GUID;
        other.BizPackageName = bp.Name;
        other.InternalSystemGUID = string.Empty;
        other.InternalSystemName = string.Empty;
        other.ExternalSystemGUID = string.Empty;
        other.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SInternalSystem)
      {
        SInternalSystem si = parentComponent as SInternalSystem;
        other.MicroserviceGUID = string.Empty;
        other.MicroserviceName = string.Empty;
        other.BizPackageGUID = string.Empty;
        other.BizPackageName = string.Empty;
        other.InternalSystemGUID = si.GUID;
        other.InternalSystemName = si.Name;
        other.ExternalSystemGUID = string.Empty;
        other.ExternalSystemName = string.Empty;
      }
      else if (parentComponent is SExternalSystem)
      {
        SExternalSystem se = parentComponent as SExternalSystem;
        other.MicroserviceGUID = string.Empty;
        other.MicroserviceName = string.Empty;
        other.BizPackageGUID = string.Empty;
        other.BizPackageName = string.Empty;
        other.InternalSystemGUID = string.Empty;
        other.InternalSystemName = string.Empty;
        other.ExternalSystemGUID = se.GUID;
        other.ExternalSystemName = se.Name;
      }

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_OTHER " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , InternalSystemGUID = @InternalSystemGUID " +
          "      , ExternalSystemGUID = @ExternalSystemGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", other.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@InternalSystemGUID", other.InternalSystemGUID);
        cmd.Parameters.AddWithValue("@ExternalSystemGUID", other.ExternalSystemGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", other.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", other.GUID);
        this.SetLastModified(cmd, other);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteOther(SOther other)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_OTHER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", other.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region BizRule

    public List<SBizRule> SelectBizRuleList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT BR.GUID " +
          "      , BR.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , BR.BizPackageGUID, B.Name AS BizPackageName " +
          "      , BR.ProgramID " +
          "      , BR.DesignCompleteSkeleton " +
          "      , BR.DesignCompleteDetail " +
          "	     , BR.Name " +
          "	     , BR.NameEnglish " +
          "	     , BR.CommonBR" +
          "	     , BR.Description " +
          "	     , BR.SpecFile" +
          "	     , BR.RegisteredDate,    BR.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    BR.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , BR.LastModifiedDate,  BR.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  BR.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_BIZ_RULE BR " +
          "        INNER JOIN TB_MICROSERVICE M ON BR.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON BR.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON BR.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON BR.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON BR.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON BR.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE BR.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE BR.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY BR.Name ";
        else
          sql += " ORDER BY BR.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SBizRule> brList = new List<SBizRule>();

      foreach (DataRow row in dt.Rows)
      {
        SBizRule br = new SBizRule();
        br.GUID = Convert.ToString(row["GUID"]);
        br.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        br.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        br.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        br.BizPackageName = Convert.ToString(row["BizPackageName"]);
        br.ProgramID = Convert.ToString(row["ProgramID"]);
        br.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        br.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        br.Name = Convert.ToString(row["Name"]);
        br.NameEnglish = Convert.ToString(row["NameEnglish"]);
        br.CommonBR = SCommon.GetBoolean(row["CommonBR"]);
        br.Description = Convert.ToString(row["Description"]);
        br.SpecFile = Convert.ToString(row["SpecFile"]);
        
        br.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        br.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        br.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        br.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        br.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        br.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        br.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        br.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        br.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        br.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        brList.Add(br);
      }

      return brList;
    }

    public SBizRule InsertBizRule(SBizPackage bp, string name = "")
    {
      SBizRule br = new SBizRule();
      br.GUID = Guid.NewGuid().ToString();
      br.MicroserviceGUID = bp.MicroserviceGUID;
      br.MicroserviceName = bp.MicroserviceName;
      br.BizPackageGUID = bp.GUID;
      br.BizPackageName = bp.Name;
      br.Name = "New BR";
      SCommon.SetDateDesigner(br, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_BIZ_RULE VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @CommonBR " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", br.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", br.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", br.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", br.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", br.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", br.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", br.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", br.NameEnglish);
        cmd.Parameters.AddWithValue("@CommonBR", br.CommonBR);
        cmd.Parameters.AddWithValue("@Description", br.Description);
        cmd.Parameters.AddWithValue("@SpecFile", br.SpecFile);
        this.SetRegistered(cmd, br);
        this.SetLastModified(cmd, br);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return br;
    }

    public void UpdateBizRule(SBizRule br)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , CommonBR = @CommonBR" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", br.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", br.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", br.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", br.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", br.NameEnglish);
        cmd.Parameters.AddWithValue("@CommonBR", br.CommonBR);
        cmd.Parameters.AddWithValue("@Description", br.Description);
        cmd.Parameters.AddWithValue("@SpecFile", br.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", br.GUID);
        this.SetLastModified(cmd, br);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizRuleLastModifiedTemp(SBizRule br)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", br.GUID);
        this.SetLastModified(cmd, br);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizRuleParent(SBizPackage bp, SBizRule br)
    {
      br.MicroserviceGUID = bp.MicroserviceGUID;
      br.MicroserviceName = bp.MicroserviceName;
      br.BizPackageGUID = bp.GUID;
      br.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", br.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", br.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", br.GUID);
        this.SetLastModified(cmd, br);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteBizRule(SBizRule br)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_BIZ_RULE WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", br.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region BizRule Operation

    public List<SBizRuleOperation> SelectBizRuleOperationList(string brGUID, string guid, string dtoGUID, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , O.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , O.BizPackageGUID, B.Name AS BizPackageName " +
          "      , O.BizRuleGUID, BR.Name AS BizRuleName " +
          "      , O.ProgramID " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.Tx " +
          "      , O.InputGUID " +
          "      , O.Input " +
          "      , O.OutputGUID " +
          "      , O.Output " +
          "	     , O.Description " +
          "	     , O.SpecFile" +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_BIZ_RULE_OPERATION O " +
          "        INNER JOIN TB_MICROSERVICE M ON BR.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON BR.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_BIZ_RULE BR ON O.BizRuleGUID = BR.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(brGUID) == false)
          sql += " WHERE O.BizRuleGUID = @BizRuleGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE O.GUID = @GUID ";
        else if (string.IsNullOrEmpty(dtoGUID) == false)
          sql += " WHERE O.InputGUID = @InputGUID OR O.OutputGUID = @OutputGUID ";

        if (sortByName)
          sql += " ORDER BY O.Name ";
        else
          sql += " ORDER BY O.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(brGUID) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@BizRuleGUID", brGUID);
        }
        else if (string.IsNullOrEmpty(guid) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        }
        else if (string.IsNullOrEmpty(dtoGUID) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@InputGUID", dtoGUID);
          adapter.SelectCommand.Parameters.AddWithValue("@OutputGUID", dtoGUID);
        }
        
        adapter.Fill(dt);
      }

      List<SBizRuleOperation> opList = new List<SBizRuleOperation>();

      foreach (DataRow row in dt.Rows)
      {
        SBizRuleOperation op = new SBizRuleOperation();

        op.GUID = Convert.ToString(row["GUID"]);
        op.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        op.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        op.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        op.BizPackageName = Convert.ToString(row["BizPackageName"]);
        op.BizRuleGUID = Convert.ToString(row["BizRuleGUID"]);
        op.BizRuleName = Convert.ToString(row["BizRuleName"]);
        op.ProgramID = Convert.ToString(row["ProgramID"]);
        op.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        op.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        op.Name = Convert.ToString(row["Name"]);
        op.NameEnglish = Convert.ToString(row["NameEnglish"]);
        op.Tx = SCommon.GetBoolean(row["Tx"]);
        op.InputGUID = Convert.ToString(row["InputGUID"]);
        op.Input = Convert.ToString(row["Input"]);
        op.OutputGUID = Convert.ToString(row["OutputGUID"]);
        op.Output = Convert.ToString(row["Output"]);
        op.Description = Convert.ToString(row["Description"]);
        op.SpecFile = Convert.ToString(row["SpecFile"]);
        
        op.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        op.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        op.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        op.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        op.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        op.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        op.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        op.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        op.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        op.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        opList.Add(op);
      }

      return opList;
    }

    public DataTable SelectBizRuleOperationList(string brName, string opName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , M.Name AS MicroserviceName" +
          "      , B.Name AS BizPackageName " +
          "      , BR.Name AS BizRuleName " +
          "      , BR.NameEnglish AS BizRuleNameEnglish " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.Tx " +
          "      , O.Input " +
          "      , O.Output " +
          "      , (SELECT COUNT(*) FROM TB_CALLEE WHERE ParentGUID = O.GUID) AS CalleeCount " + 
          "	     , O.Description " +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_BIZ_RULE_OPERATION O " +
          "        INNER JOIN TB_MICROSERVICE M ON BR.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON BR.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_BIZ_RULE BR ON O.BizRuleGUID = BR.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID " +
          "  WHERE BR.Name LIKE @BRName " + 
          "    AND O.Name LIKE @OPName " +
          " ORDER BY M.Name, B.Name, BR.Name, O.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@BRName", "%" + brName + "%");
        adapter.SelectCommand.Parameters.AddWithValue("@OPName", "%" + opName + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertBizRuleOperation(SBizRuleOperation brOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_BIZ_RULE_OPERATION VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @BizRuleGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Tx " +
          "  , @InputGUID " +
          "  , @Input " +
          "  , @OutputGUID " +
          "  , @Output " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", brOp.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", brOp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", brOp.BizPackageGUID);
        cmd.Parameters.AddWithValue("@BizRuleGUID", brOp.BizRuleGUID);
        cmd.Parameters.AddWithValue("@ProgramID", brOp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", brOp.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", brOp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", brOp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", brOp.NameEnglish);
        cmd.Parameters.AddWithValue("@Tx", brOp.Tx);
        cmd.Parameters.AddWithValue("@InputGUID", brOp.InputGUID);
        cmd.Parameters.AddWithValue("@Input", brOp.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", brOp.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", brOp.Output);
        cmd.Parameters.AddWithValue("@Description", brOp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", brOp.SpecFile);
        this.SetRegistered(cmd, brOp);
        this.SetLastModified(cmd, brOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizRuleOperation(SBizRuleOperation brOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE_OPERATION " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , Tx = @Tx " +
          "      , InputGUID = @InputGUID " +
          "      , Input = @Input " +
          "      , OutputGUID = @OutputGUID " +
          "      , Output = @Output " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", brOp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", brOp.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", brOp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", brOp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", brOp.NameEnglish);
        cmd.Parameters.AddWithValue("@Tx", brOp.Tx);
        cmd.Parameters.AddWithValue("@InputGUID", brOp.InputGUID);
        cmd.Parameters.AddWithValue("@Input", brOp.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", brOp.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", brOp.Output);
        cmd.Parameters.AddWithValue("@Description", brOp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", brOp.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", brOp.GUID);
        this.SetLastModified(cmd, brOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizRuleOperationLastModifiedTemp(SBizRuleOperation brOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE_OPERATION " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", brOp.GUID);
        this.SetLastModified(cmd, brOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateBizRuleOperationParent(SBizRule br, SBizRuleOperation brOp)
    {
      brOp.MicroserviceGUID = br.MicroserviceGUID;
      brOp.MicroserviceName = br.MicroserviceName;
      brOp.BizPackageGUID = br.BizPackageGUID;
      brOp.BizPackageName = br.BizPackageName;
      brOp.BizRuleGUID = br.GUID;
      brOp.BizRuleName = br.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_BIZ_RULE_OPERATION " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , BizRuleGUID = @BizRuleGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", brOp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", brOp.BizPackageGUID);
        cmd.Parameters.AddWithValue("@BizRuleGUID", brOp.BizRuleGUID);
        cmd.Parameters.AddWithValue("@GUID", brOp.GUID);
        this.SetLastModified(cmd, brOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }


    public void DeleteBizRuleOperation(SBizRuleOperation brOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_BIZ_RULE_OPERATION WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", brOp.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Callee

    public List<SCallee> SelectCalleeList(string parentGUID)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT ParentComponentType " +
          "      , ParentGUID " +
          "      , CalleeComponentType " +
          "      , CalleeGUID " +
          "      , CalleeFullPath " +
          "      , Comment " +
          "	  FROM TB_CALLEE " +
          "  WHERE ParentGUID = @ParentGUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@ParentGUID", parentGUID);
        adapter.Fill(dt);
      }

      List<SCallee> calleeList = new List<SCallee>();

      foreach (DataRow row in dt.Rows)
      {
        SCallee callee = new SCallee();

        //parent
        string parentComponentType = Convert.ToString(row["ParentComponentType"]);

        if (SComponentType.BizRuleOperation.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.BizRuleOperation;
        else if (SComponentType.InternalSystem.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.InternalSystem;
        else if (SComponentType.ExternalSystem.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.ExternalSystem;

        callee.ParentGUID = Convert.ToString(row["ParentGUID"]);

        //callee
        string calleeComponentType = Convert.ToString(row["CalleeComponentType"]);

        if (SComponentType.API.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.API;
        else if (SComponentType.BizRuleOperation.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.BizRuleOperation;
        else if (SComponentType.DataAccessOperation.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.DataAccessOperation;
        else if (SComponentType.Publisher.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.Publisher;
        else if (SComponentType.Other.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.Other;

        callee.CalleeGUID = Convert.ToString(row["CalleeGUID"]);
        callee.CalleeFullPath = Convert.ToString(row["CalleeFullPath"]);
        callee.Comment = Convert.ToString(row["Comment"]);
        calleeList.Add(callee);
      }

      return calleeList;
    }

    public List<SCallee> SelectCalleeListByCallee(SComponentType parentComponentTypeInput, string calleeGUID)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT ParentComponentType " +
          "      , ParentGUID " +
          "      , CalleeComponentType " +
          "      , CalleeGUID " +
          "      , CalleeFullPath " +
          "      , Comment " +
          "	  FROM TB_CALLEE " +
          "  WHERE ParentComponentType = @ParentComponentType " +
          "    AND CalleeGUID = @CalleeGUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@ParentComponentType", parentComponentTypeInput.ToString());
        adapter.SelectCommand.Parameters.AddWithValue("@CalleeGUID", calleeGUID);
        adapter.Fill(dt);
      }

      List<SCallee> calleeList = new List<SCallee>();

      foreach (DataRow row in dt.Rows)
      {
        SCallee callee = new SCallee();

        //parent
        string parentComponentType = Convert.ToString(row["ParentComponentType"]);

        if (SComponentType.BizRuleOperation.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.BizRuleOperation;
        else if (SComponentType.InternalSystem.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.InternalSystem;
        else if (SComponentType.ExternalSystem.ToString() == parentComponentType)
          callee.ParentComponentType = SComponentType.ExternalSystem;

        callee.ParentGUID = Convert.ToString(row["ParentGUID"]);

        //callee
        string calleeComponentType = Convert.ToString(row["CalleeComponentType"]);

        if (SComponentType.API.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.API;
        else if (SComponentType.BizRuleOperation.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.BizRuleOperation;
        else if (SComponentType.DataAccessOperation.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.DataAccessOperation;
        else if (SComponentType.Publisher.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.Publisher;
        else if (SComponentType.Other.ToString() == calleeComponentType)
          callee.CalleeComponentType = SComponentType.Other;

        callee.CalleeGUID = Convert.ToString(row["CalleeGUID"]);
        callee.CalleeFullPath = Convert.ToString(row["CalleeFullPath"]);
        callee.Comment = Convert.ToString(row["Comment"]);
        calleeList.Add(callee);
      }

      return calleeList;
    }


    public void InsertCallee(SCallee callee)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_CALLEE VALUES " +
          " (  @ParentComponentType " +
          "  , @ParentGUID " +
          "  , @CalleeComponentType " +
          "  , @CalleeGUID " +
          "  , @CalleeFullPath " +
          "  , @Comment " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);

        cmd.Parameters.AddWithValue("@ParentComponentType", callee.ParentComponentType.ToString());
        cmd.Parameters.AddWithValue("@ParentGUID", callee.ParentGUID);
        cmd.Parameters.AddWithValue("@CalleeComponentType", callee.CalleeComponentType.ToString());
        cmd.Parameters.AddWithValue("@CalleeGUID", callee.CalleeGUID);
        cmd.Parameters.AddWithValue("@CalleeFullPath", callee.CalleeFullPath);
        cmd.Parameters.AddWithValue("@Comment", callee.Comment);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteCalleeList(string parentGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_CALLEE WHERE ParentGUID = @ParentGUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ParentGUID", parentGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region DataAccess

    public List<SDataAccess> SelectDataAccessList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT DA.GUID " +
          "      , DA.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , DA.BizPackageGUID, B.Name AS BizPackageName " +
          "      , DA.ProgramID " +
          "      , DA.DesignCompleteSkeleton " +
          "      , DA.DesignCompleteDetail " +
          "	     , DA.Name " +
          "	     , DA.NameEnglish " +
          "	     , DA.JoinDA" +
          "	     , DA.Description " +
          "	     , DA.SpecFile" +
          "	     , DA.RegisteredDate,    DA.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    DA.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , DA.LastModifiedDate,  DA.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  DA.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_DATA_ACCESS DA " +
          "        INNER JOIN TB_MICROSERVICE M ON DA.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON DA.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON DA.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON DA.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON DA.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON DA.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE DA.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE DA.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY DA.Name ";
        else
          sql += " ORDER BY DA.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        
        adapter.Fill(dt);
      }

      List<SDataAccess> daList = new List<SDataAccess>();

      foreach (DataRow row in dt.Rows)
      {
        SDataAccess da = new SDataAccess();
        da.GUID = Convert.ToString(row["GUID"]);
        da.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        da.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        da.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        da.BizPackageName = Convert.ToString(row["BizPackageName"]);
        da.ProgramID = Convert.ToString(row["ProgramID"]);
        da.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        da.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        da.Name = Convert.ToString(row["Name"]);
        da.NameEnglish = Convert.ToString(row["NameEnglish"]);
        da.JoinDA = SCommon.GetBoolean(row["JoinDA"]);
        da.Description = Convert.ToString(row["Description"]);
        da.SpecFile = Convert.ToString(row["SpecFile"]);
        
        da.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        da.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        da.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        da.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        da.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        da.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        da.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        da.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        da.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        da.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        daList.Add(da);
      }

      return daList;
    }

    public SDataAccess InsertDataAccess(SBizPackage bp, string name = "")
    {
      SDataAccess da = new SDataAccess();
      da.GUID = Guid.NewGuid().ToString();
      da.MicroserviceGUID = bp.MicroserviceGUID;
      da.MicroserviceName = bp.MicroserviceName;
      da.BizPackageGUID = bp.GUID;
      da.BizPackageName = bp.Name;
      da.Name = "New DA";
      SCommon.SetDateDesigner(da, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_DATA_ACCESS VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @JoinDA " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", da.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", da.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", da.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", da.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", da.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", da.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", da.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", da.NameEnglish);
        cmd.Parameters.AddWithValue("@JoinDA", da.JoinDA);
        cmd.Parameters.AddWithValue("@Description", da.Description);
        cmd.Parameters.AddWithValue("@SpecFile", da.SpecFile);
        this.SetRegistered(cmd, da);
        this.SetLastModified(cmd, da);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return da;
    }

    public void UpdateDataAccess(SDataAccess da)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , JoinDA = @JoinDA" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", da.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", da.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", da.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", da.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", da.NameEnglish);
        cmd.Parameters.AddWithValue("@JoinDA", da.JoinDA);
        cmd.Parameters.AddWithValue("@Description", da.Description);
        cmd.Parameters.AddWithValue("@SpecFile", da.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", da.GUID);
        this.SetLastModified(cmd, da);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDataAccessLastModifiedTemp(SDataAccess da)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", da.GUID);
        this.SetLastModified(cmd, da);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDataAccessParent(SBizPackage bp, SDataAccess da)
    {
      da.MicroserviceGUID = bp.MicroserviceGUID;
      da.MicroserviceName = bp.MicroserviceName;
      da.BizPackageGUID = bp.GUID;
      da.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", da.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", da.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", da.GUID);
        this.SetLastModified(cmd, da);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteDataAccess(SDataAccess da)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_DATA_ACCESS WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", da.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region DataAccess Operation

    public List<SDataAccessOperation> SelectDataAccessOperationList(string daGUID, string guid, string entityGUID, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , O.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , O.BizPackageGUID, B.Name AS BizPackageName " +
          "      , O.DataAccessGUID, DA.Name AS DataAccessName " +
          "      , O.ProgramID " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.InputGUID " +
          "      , O.Input " +
          "      , O.OutputGUID " +
          "      , O.Output " +
          "      , O.SQL " +
          "	     , O.Description " +
          "	     , O.SpecFile" +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_DATA_ACCESS_OPERATION O " +
          "        INNER JOIN TB_MICROSERVICE M ON O.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON O.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_DATA_ACCESS DA ON O.DataAccessGUID = DA.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(daGUID) == false)
          sql += " WHERE O.DataAccessGUID = @DataAccessGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE O.GUID = @GUID ";
        else if (string.IsNullOrEmpty(entityGUID) == false)
          sql += " WHERE O.InputGUID = @InputGUID OR O.OutputGUID = @OutputGUID ";

        if (sortByName)
          sql += " ORDER BY O.Name ";
        else
          sql += " ORDER BY O.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(daGUID) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@DataAccessGUID", daGUID);
        }
        else if (string.IsNullOrEmpty(guid) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        }
        else if (string.IsNullOrEmpty(entityGUID) == false)
        {
          adapter.SelectCommand.Parameters.AddWithValue("@InputGUID", entityGUID);
          adapter.SelectCommand.Parameters.AddWithValue("@OutputGUID", entityGUID);
        }

        adapter.Fill(dt);
      }

      List<SDataAccessOperation> opList = new List<SDataAccessOperation>();

      foreach (DataRow row in dt.Rows)
      {
        SDataAccessOperation op = new SDataAccessOperation();

        op.GUID = Convert.ToString(row["GUID"]);
        op.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        op.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        op.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        op.BizPackageName = Convert.ToString(row["BizPackageName"]);
        op.DataAccessGUID = Convert.ToString(row["DataAccessGUID"]);
        op.DataAccessName = Convert.ToString(row["DataAccessName"]);
        op.ProgramID = Convert.ToString(row["ProgramID"]);
        op.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        op.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        op.Name = Convert.ToString(row["Name"]);
        op.NameEnglish = Convert.ToString(row["NameEnglish"]);
        op.InputGUID = Convert.ToString(row["InputGUID"]);
        op.Input = Convert.ToString(row["Input"]);
        op.OutputGUID = Convert.ToString(row["OutputGUID"]);
        op.Output = Convert.ToString(row["Output"]);
        op.SQL = Convert.ToString(row["SQL"]);
        op.Description = Convert.ToString(row["Description"]);
        op.SpecFile = Convert.ToString(row["SpecFile"]);
        
        op.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        op.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        op.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        op.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        op.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        op.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        op.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        op.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        op.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        op.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        opList.Add(op);
      }

      return opList;
    }

    public DataTable SelectDataAccessOperationList(string daName, string opName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT O.GUID " +
          "      , M.Name AS MicroserviceName" +
          "      , B.Name AS BizPackageName " +
          "      , DA.Name AS DataAccessName " +
          "      , DA.NameEnglish AS DataAccessNameEnglish " +
          "      , O.DesignCompleteSkeleton " +
          "      , O.DesignCompleteDetail " +
          "	     , O.Name " +
          "	     , O.NameEnglish " +
          "      , O.Input " +
          "      , O.Output " +
          "	     , O.Description " +
          "      , O.SQL " +
          "	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_DATA_ACCESS_OPERATION O " +
          "        INNER JOIN TB_MICROSERVICE M ON O.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON O.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_DATA_ACCESS DA ON O.DataAccessGUID = DA.GUID " +
          "        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID " +
          "  WHERE DA.Name LIKE @DAName " +
          "    AND O.Name LIKE @OPName " +
          " ORDER BY M.Name, B.Name, DA.Name, O.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@DAName", "%" + daName + "%");
        adapter.SelectCommand.Parameters.AddWithValue("@OPName", "%" + opName + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertDataAccessOperation(SDataAccessOperation daOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_DATA_ACCESS_OPERATION VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @DataAccessGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @InputGUID " +
          "  , @Input " +
          "  , @OutputGUID " +
          "  , @Output " +
          "  , @SQL " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", daOp.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", daOp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", daOp.BizPackageGUID);
        cmd.Parameters.AddWithValue("@DataAccessGUID", daOp.DataAccessGUID);
        cmd.Parameters.AddWithValue("@ProgramID", daOp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", daOp.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", daOp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", daOp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", daOp.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", daOp.InputGUID);
        cmd.Parameters.AddWithValue("@Input", daOp.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", daOp.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", daOp.Output);
        cmd.Parameters.AddWithValue("@SQL", daOp.SQL);
        cmd.Parameters.AddWithValue("@Description", daOp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", daOp.SpecFile);
        this.SetRegistered(cmd, daOp);
        this.SetLastModified(cmd, daOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDataAccessOperation(SDataAccessOperation daOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS_OPERATION " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , InputGUID = @InputGUID " +
          "      , Input = @Input " +
          "      , OutputGUID = @OutputGUID " +
          "      , Output = @Output " +
          "      , SQL = @SQL " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", daOp.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", daOp.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", daOp.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", daOp.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", daOp.NameEnglish);
        cmd.Parameters.AddWithValue("@InputGUID", daOp.InputGUID);
        cmd.Parameters.AddWithValue("@Input", daOp.Input);
        cmd.Parameters.AddWithValue("@OutputGUID", daOp.OutputGUID);
        cmd.Parameters.AddWithValue("@Output", daOp.Output);
        cmd.Parameters.AddWithValue("@SQL", daOp.SQL);
        cmd.Parameters.AddWithValue("@Description", daOp.Description);
        cmd.Parameters.AddWithValue("@SpecFile", daOp.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", daOp.GUID);
        this.SetLastModified(cmd, daOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDataAccessOperationLastModifiedTemp(SDataAccessOperation daOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS_OPERATION " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", daOp.GUID);
        this.SetLastModified(cmd, daOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDataAccessOperationParent(SDataAccess da, SDataAccessOperation daOp)
    {
      daOp.MicroserviceGUID = da.MicroserviceGUID;
      daOp.MicroserviceName = da.MicroserviceName;
      daOp.BizPackageGUID = da.BizPackageGUID;
      daOp.BizPackageName = da.BizPackageName;
      daOp.DataAccessGUID = da.GUID;
      daOp.DataAccessName = da.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DATA_ACCESS_OPERATION " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , DataAccessGUID = @DataAccessGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", daOp.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", daOp.BizPackageGUID);
        cmd.Parameters.AddWithValue("@DataAccessGUID", daOp.DataAccessGUID);
        cmd.Parameters.AddWithValue("@GUID", daOp.GUID);
        this.SetLastModified(cmd, daOp);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteDataAccessOperation(SDataAccessOperation daOp)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_DATA_ACCESS_OPERATION WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", daOp.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Dto

    public List<SDto> SelectDtoList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT D.GUID " +
          "      , D.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , D.BizPackageGUID, B.Name AS BizPackageName " +
          "      , D.ProgramID " +
          "      , D.DesignCompleteSkeleton " +
          "      , D.DesignCompleteDetail " +
          "	     , D.Name " +
          "	     , D.NameEnglish " +
          "	     , D.Description " +
          "	     , D.SpecFile" +
          "	     , D.RegisteredDate,    D.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    D.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , D.LastModifiedDate,  D.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  D.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_DTO D " +
          "        INNER JOIN TB_MICROSERVICE M ON D.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON D.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON D.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON D.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON D.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON D.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE D.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE D.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY D.Name ";
        else
          sql += " ORDER BY D.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SDto> dtoList = new List<SDto>();

      foreach (DataRow row in dt.Rows)
      {
        SDto dto = new SDto();
        dto.GUID = Convert.ToString(row["GUID"]);
        dto.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        dto.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        dto.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        dto.BizPackageName = Convert.ToString(row["BizPackageName"]);
        dto.ProgramID = Convert.ToString(row["ProgramID"]);
        dto.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        dto.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        dto.Name = Convert.ToString(row["Name"]);
        dto.NameEnglish = Convert.ToString(row["NameEnglish"]);
        dto.Description = Convert.ToString(row["Description"]);
        dto.SpecFile = Convert.ToString(row["SpecFile"]);
         
        dto.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        dto.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        dto.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        dto.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        dto.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
         
        dto.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        dto.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        dto.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        dto.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        dto.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
         
        dtoList.Add(dto);
      }

      return dtoList;
    }
  
    public SDto InsertDto(SBizPackage bp, string name = "")
    {
      SDto dto = new SDto();
      dto.MicroserviceGUID = bp.MicroserviceGUID;
      dto.MicroserviceName = bp.MicroserviceName;
      dto.BizPackageGUID = bp.GUID;
      dto.BizPackageName = bp.Name;
      dto.GUID = Guid.NewGuid().ToString();
      dto.Name = "New Dto";
      SCommon.SetDateDesigner(dto, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_DTO VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", dto.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", dto.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", dto.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", dto.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", dto.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", dto.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", dto.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", dto.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", dto.Description);
        cmd.Parameters.AddWithValue("@SpecFile", dto.SpecFile);
        this.SetRegistered(cmd, dto);
        this.SetLastModified(cmd, dto);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return dto;
    }

    public void UpdateDto(SDto dto)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DTO " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton " +
          "      , DesignCompleteDetail = @DesignCompleteDetail " +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", dto.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", dto.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", dto.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", dto.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", dto.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", dto.Description);
        cmd.Parameters.AddWithValue("@SpecFile", dto.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", dto.GUID);
        this.SetLastModified(cmd, dto);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDtoLastModifiedTemp(SDto dto)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DTO " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", dto.GUID);
        this.SetLastModified(cmd, dto);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDtoParent(SBizPackage bp, SDto dto)
    {
      dto.MicroserviceGUID = bp.MicroserviceGUID;
      dto.MicroserviceName = bp.MicroserviceName;
      dto.BizPackageGUID = bp.GUID;
      dto.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DTO " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", dto.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", dto.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", dto.GUID);
        this.SetLastModified(cmd, dto);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteDto(SDto dto)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_DTO WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", dto.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Dto Attribute

    public List<SDtoAttribute> SelectDtoAttributeList(string dtoGUID)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT DA.GUID " +
          "      , DA.MicroserviceGUID, M.Name AS MicroserviceName " +
          "      , DA.BizPackageGUID, B.Name AS BizPackageName " +
          "      , DA.DtoGUID, D.Name AS DtoName " +
          "      , DA.ProgramID " +
          "      , DA.DataType " +
          "	     , DA.Name " +
          "	     , DA.NameEnglish " +
          "	     , DA.Description " +
          "	  FROM TB_DTO_ATTRIBUTE DA " +
          "        INNER JOIN TB_MICROSERVICE M ON DA.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON DA.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_DTO D ON DA.DtoGUID = D.GUID " +
          "  WHERE DA.DtoGUID = @DtoGUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@DtoGUID", dtoGUID);
        adapter.Fill(dt);
      }

      List<SDtoAttribute> attrList = new List<SDtoAttribute>();

      foreach (DataRow row in dt.Rows)
      {
        SDtoAttribute attr = new SDtoAttribute();
        attr.GUID = Convert.ToString(row["GUID"]);
        attr.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        attr.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        attr.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        attr.BizPackageName = Convert.ToString(row["BizPackageName"]);
        attr.DtoGUID = Convert.ToString(row["DtoGUID"]);
        attr.DtoName = Convert.ToString(row["DtoName"]);
        attr.ProgramID = Convert.ToString(row["ProgramID"]);
        attr.DataType = Convert.ToString(row["DataType"]);
        attr.Name = Convert.ToString(row["Name"]);
        attr.NameEnglish = Convert.ToString(row["NameEnglish"]);
        attr.Description = Convert.ToString(row["Description"]);
        attrList.Add(attr);
      }

      return attrList;
    }

    public DataTable SelectDtoAttributeList(string dtoName, string attrName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT DA.GUID " +
          "      , M.Name AS MicroserviceName " +
          "      , B.Name AS BizPackageName " +
          "      , D.Name AS DtoName " +
          "      , D.NameEnglish AS DtoNameEnglish " +
          "      , DA.DataType " +
          "	     , DA.Name " +
          "	     , DA.NameEnglish " +
          "	     , DA.Description " +
          //Attr은 등록/수정이 동일할 것이고, Dto의 등록/수정정보를 보여주기도 애매하여 제외
          //"	     , O.RegisteredDate,    O.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    O.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          //"	     , O.LastModifiedDate,  O.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  O.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_DTO_ATTRIBUTE DA " +
          "        INNER JOIN TB_MICROSERVICE M ON DA.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON DA.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_DTO D ON DA.DtoGUID = D.GUID " +
          //"        INNER JOIN TB_PART P1 ON O.RegisteredPartGUID = P1.GUID " +
          //"        INNER JOIN TB_PART P2 ON O.LastModifiedPartGUID = P2.GUID " +
          //"        INNER JOIN TB_USER U1 ON O.RegisteredUserGUID = U1.GUID " +
          //"        INNER JOIN TB_USER U2 ON O.LastModifiedUserGUID = U2.GUID " +
          "  WHERE D.Name LIKE @DtoName " +
          "    AND DA.Name LIKE @AttrName " +
          " ORDER BY M.Name, B.Name, D.Name, DA.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@DtoName", "%" + dtoName + "%");
        adapter.SelectCommand.Parameters.AddWithValue("@AttrName", "%" + attrName + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertDtoAttribute(SDto dto, string programId, string dataType, string name, string variable, string description)
    {
      SDtoAttribute attribute = new SDtoAttribute();
      attribute.MicroserviceGUID = dto.MicroserviceGUID;
      attribute.MicroserviceName = dto.MicroserviceName;
      attribute.BizPackageGUID = dto.BizPackageGUID;
      attribute.BizPackageName = dto.BizPackageName;
      attribute.DtoGUID = dto.GUID;
      attribute.DtoName = SCommon.GetName(dto.Name);
      attribute.GUID = Guid.NewGuid().ToString();
      attribute.ProgramID = programId;
      attribute.DataType = dataType;
      attribute.Name = name;
      attribute.NameEnglish = variable;
      attribute.Description = description;
      SCommon.SetDateDesigner(attribute, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_DTO_ATTRIBUTE VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @DtoGUID " +
          "  , @ProgramID " +
          "  , @DataType " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);

        cmd.Parameters.AddWithValue("@GUID", attribute.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", attribute.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", attribute.BizPackageGUID);
        cmd.Parameters.AddWithValue("@DtoGUID", attribute.DtoGUID);
        cmd.Parameters.AddWithValue("@ProgramID", attribute.ProgramID);
        cmd.Parameters.AddWithValue("@DataType", attribute.DataType);
        cmd.Parameters.AddWithValue("@Name", attribute.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", attribute.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", attribute.Description);
        this.SetRegistered(cmd, attribute);
        this.SetLastModified(cmd, attribute);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateDtoAttributeParent(SDto dto, SDtoAttribute attribute)
    {
      attribute.MicroserviceGUID = dto.MicroserviceGUID;
      attribute.MicroserviceName = dto.MicroserviceName;
      attribute.BizPackageGUID = dto.BizPackageGUID;
      attribute.BizPackageName = dto.BizPackageName;
      attribute.DtoGUID = dto.GUID;
      attribute.DtoName = dto.Name;
      SCommon.SetDateDesigner(attribute, false);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_DTO_ATTRIBUTE " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , DtoGUID = @DtoGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", attribute.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", attribute.BizPackageGUID);
        cmd.Parameters.AddWithValue("@DtoGUID", attribute.DtoGUID);
        cmd.Parameters.AddWithValue("@GUID", attribute.GUID);
        this.SetLastModified(cmd, attribute);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteDtoAttributeList(string dtoGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_DTO_ATTRIBUTE WHERE DtoGUID = @DtoGUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@DtoGUID", dtoGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Entity

    public List<SEntity> SelectEntityList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT E.GUID " +
          "      , E.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , E.BizPackageGUID, B.Name AS BizPackageName " +
          "      , E.ProgramID " +
          "      , E.DesignCompleteSkeleton " +
          "      , E.DesignCompleteDetail " +
          "	     , E.Name " +
          "	     , E.NameEnglish " +
          "      , E.JoinEntity " +
          "      , E.TableName " +
          "	     , E.Description " +
          "	     , E.SpecFile" +
          "	     , E.RegisteredDate,    E.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    E.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , E.LastModifiedDate,  E.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  E.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_ENTITY E " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON E.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON E.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON E.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON E.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON E.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON E.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE E.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE E.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY E.Name ";
        else
          sql += " ORDER BY E.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);
        
        adapter.Fill(dt);
      }

      List<SEntity> entityList = new List<SEntity>();

      foreach (DataRow row in dt.Rows)
      {
        SEntity entity = new SEntity();
        entity.GUID = Convert.ToString(row["GUID"]);
        entity.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        entity.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        entity.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        entity.BizPackageName = Convert.ToString(row["BizPackageName"]);
        entity.ProgramID = Convert.ToString(row["ProgramID"]);
        entity.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        entity.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        entity.Name = Convert.ToString(row["Name"]);
        entity.NameEnglish = Convert.ToString(row["NameEnglish"]);
        entity.JoinEntity = SCommon.GetBoolean(row["JoinEntity"]);
        entity.TableName = Convert.ToString(row["TableName"]);
        entity.Description = Convert.ToString(row["Description"]);
        entity.SpecFile = Convert.ToString(row["SpecFile"]);
        
        entity.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        entity.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        entity.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        entity.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        entity.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        entity.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        entity.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        entity.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        entity.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        entity.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        entityList.Add(entity);
      }

      return entityList;
    }
  
    public SEntity InsertEntity(SBizPackage bp, string name = "")
    {
      SEntity entity = new SEntity();
      entity.GUID = Guid.NewGuid().ToString();
      entity.MicroserviceGUID = bp.MicroserviceGUID;
      entity.MicroserviceName = bp.MicroserviceName;
      entity.BizPackageGUID = bp.GUID;
      entity.BizPackageName = bp.Name;
      entity.Name = "New Entity";
      SCommon.SetDateDesigner(entity, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_ENTITY VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @JoinEntity " +
          "  , @TableName " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", entity.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", entity.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", entity.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", entity.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", entity.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", entity.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", entity.NameEnglish);
        cmd.Parameters.AddWithValue("@JoinEntity", entity.JoinEntity);
        cmd.Parameters.AddWithValue("@TableName", entity.TableName);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.Parameters.AddWithValue("@SpecFile", entity.SpecFile);
        this.SetRegistered(cmd, entity);
        this.SetLastModified(cmd, entity);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return entity;
    }

    public void UpdateEntity(SEntity entity)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_ENTITY " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish" +
          "      , JoinEntity = @JoinEntity" +
          "      , TableName = @TableName" +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", entity.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", entity.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", entity.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", entity.NameEnglish);
        cmd.Parameters.AddWithValue("@JoinEntity", entity.JoinEntity);
        cmd.Parameters.AddWithValue("@TableName", entity.TableName);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.Parameters.AddWithValue("@SpecFile", entity.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", entity.GUID);
        this.SetLastModified(cmd, entity);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateEntityLastModifiedTemp(SEntity entity)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_ENTITY " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", entity.GUID);
        this.SetLastModified(cmd, entity);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateEntityParent(SBizPackage bp, SEntity entity)
    {
      entity.MicroserviceGUID = bp.MicroserviceGUID;
      entity.MicroserviceName = bp.MicroserviceName;
      entity.BizPackageGUID = bp.GUID;
      entity.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_ENTITY " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", entity.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", entity.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", entity.GUID);
        this.SetLastModified(cmd, entity);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteEntity(SEntity entity)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_ENTITY WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", entity.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Entity Attribute

    public List<SEntityAttribute> SelectEntityAttributeList(string entityGUID)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT EA.GUID " +
          "      , EA.MicroserviceGUID, M.Name AS MicroserviceName " +
          "      , EA.BizPackageGUID, B.Name AS BizPackageName " +
          "      , EA.EntityGUID, E.Name AS EntityName " +
          "      , EA.ProgramID " +
          "      , EA.DataType " +
          "	     , EA.Name " +
          "	     , EA.NameEnglish " +
          "      , EA.PK " +
          "      , EA.FK " +
          "      , EA.NN " +
          "      , EA.DBDataType " +
          "      , EA.DBColumn " +
          "	     , EA.Description " +
          "	  FROM TB_ENTITY_ATTRIBUTE EA " +
          "        INNER JOIN TB_MICROSERVICE M ON EA.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON EA.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_ENTITY E ON EA.EntityGUID = E.GUID " +
          "  WHERE EA.EntityGUID = @EntityGUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@EntityGUID", entityGUID);
        adapter.Fill(dt);
      }

      List<SEntityAttribute> attrList = new List<SEntityAttribute>();

      foreach (DataRow row in dt.Rows)
      {
        SEntityAttribute attr = new SEntityAttribute();
        attr.GUID = Convert.ToString(row["GUID"]);
        attr.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        attr.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        attr.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        attr.BizPackageName = Convert.ToString(row["BizPackageName"]);
        attr.EntityGUID = Convert.ToString(row["EntityGUID"]);
        attr.EntityName = Convert.ToString(row["EntityName"]);
        attr.ProgramID = Convert.ToString(row["ProgramID"]);
        attr.DataType = Convert.ToString(row["DataType"]);
        attr.Name = Convert.ToString(row["Name"]);
        attr.NameEnglish = Convert.ToString(row["NameEnglish"]);
        attr.PK = SCommon.GetBoolean(row["PK"]);
        attr.FK = SCommon.GetBoolean(row["FK"]);
        attr.NN = SCommon.GetBoolean(row["NN"]);
        attr.DBDataType = Convert.ToString(row["DBDataType"]);
        attr.DBColumn = Convert.ToString(row["DBColumn"]);
        attr.Description = Convert.ToString(row["Description"]);
        attrList.Add(attr);
      }

      return attrList;
    }

    public DataTable SelectEntityAttributeList(string entityName, string attrName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT EA.GUID " +
          "      , M.Name AS MicroserviceName " +
          "      , B.Name AS BizPackageName " +
          "      , E.Name AS EntityName " +
          "      , E.NameEnglish AS EntityNameEnglish " +
          "      , E.TableName AS EntityTableName " +
          "      , EA.DataType " +
          "	     , EA.Name " +
          "	     , EA.NameEnglish " +
          "      , EA.DBDataType " +
          "      , EA.DBColumn " +
          "	     , EA.Description " +
          "	  FROM TB_ENTITY_ATTRIBUTE EA " +
          "        INNER JOIN TB_MICROSERVICE M ON EA.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON EA.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_ENTITY E ON EA.EntityGUID = E.GUID " +
          "  WHERE E.Name LIKE @EntityName " +
          "    AND EA.Name LIKE @AttrName " +
          " ORDER BY M.Name, B.Name, E.Name, EA.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@EntityName", "%" + entityName + "%");
        adapter.SelectCommand.Parameters.AddWithValue("@AttrName", "%" + attrName + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public void InsertEntityAttribute(SEntity entity, string programId, string dataType, string name, string variable, bool pk, bool fk, bool nn, string dbDataType, string dbColumn, string description)
    {
      SEntityAttribute attribute = new SEntityAttribute();
      attribute.MicroserviceGUID = entity.MicroserviceGUID;
      attribute.MicroserviceName = entity.MicroserviceName;
      attribute.BizPackageGUID = entity.BizPackageGUID;
      attribute.BizPackageName = entity.BizPackageName;
      attribute.EntityGUID = entity.GUID;
      attribute.EntityName = SCommon.GetName(entity.Name);
      attribute.GUID = Guid.NewGuid().ToString();
      attribute.ProgramID = programId;
      attribute.DataType = dataType;
      attribute.Name = name;
      attribute.NameEnglish = variable;
      attribute.PK = pk;
      attribute.FK = fk;
      attribute.NN = nn;
      attribute.DBDataType = dbDataType;
      attribute.DBColumn = dbColumn;
      attribute.Description = description;
      SCommon.SetDateDesigner(attribute, true);

      //id, dt, attr, var, pk, fk, nn, *db dt, *db col, desc
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_ENTITY_ATTRIBUTE VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @EntityGUID " +
          "  , @ProgramID " +
          "  , @DataType " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @PK " +
          "  , @FK " +
          "  , @NN " +
          "  , @DBDataType " +
          "  , @DBColumn " +
          "  , @Description " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);

        cmd.Parameters.AddWithValue("@GUID", attribute.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", attribute.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", attribute.BizPackageGUID);
        cmd.Parameters.AddWithValue("@EntityGUID", attribute.EntityGUID);
        cmd.Parameters.AddWithValue("@ProgramID", attribute.ProgramID);
        cmd.Parameters.AddWithValue("@DataType", attribute.DataType);
        cmd.Parameters.AddWithValue("@Name", attribute.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", attribute.NameEnglish);
        cmd.Parameters.AddWithValue("@PK", attribute.PK);
        cmd.Parameters.AddWithValue("@FK", attribute.FK);
        cmd.Parameters.AddWithValue("@NN", attribute.NN);
        cmd.Parameters.AddWithValue("@DBDataType", attribute.DBDataType);
        cmd.Parameters.AddWithValue("@DBColumn", attribute.DBColumn);
        cmd.Parameters.AddWithValue("@Description", attribute.Description);
        this.SetRegistered(cmd, attribute);
        this.SetLastModified(cmd, attribute);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateEntityAttributeParent(SEntity entity, SEntityAttribute attribute)
    {
      attribute.MicroserviceGUID = entity.MicroserviceGUID;
      attribute.MicroserviceName = entity.MicroserviceName;
      attribute.BizPackageGUID = entity.BizPackageGUID;
      attribute.BizPackageName = entity.BizPackageName;
      attribute.EntityGUID = entity.GUID;
      attribute.EntityName = entity.Name;
      SCommon.SetDateDesigner(attribute, false);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_ENTITY_ATTRIBUTE " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , EntityGUID = @EntityGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", attribute.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", attribute.BizPackageGUID);
        cmd.Parameters.AddWithValue("@EntityGUID", attribute.EntityGUID);
        cmd.Parameters.AddWithValue("@GUID", attribute.GUID);
        this.SetLastModified(cmd, attribute);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteEntityAttributeList(string entityGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_ENTITY_ATTRIBUTE WHERE EntityGUID = @EntityGUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@EntityGUID", entityGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region UI

    public List<SUI> SelectUIList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT U.GUID " +
          "      , U.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , U.BizPackageGUID, B.Name AS BizPackageName " +
          "      , U.ProgramID " +
          "      , U.DesignCompleteSkeleton " +
          "      , U.DesignCompleteDetail " +
          "	     , U.Name " +
          "	     , U.NameEnglish " +
          "      , U.UIType " +
          "	     , U.Description " +
          "	     , U.SpecFile" +
          "	     , U.RegisteredDate,    U.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    U.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , U.LastModifiedDate,  U.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  U.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_UI U " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON U.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON U.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON U.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON U.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON U.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON U.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE U.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE U.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY U.Name ";
        else
          sql += " ORDER BY U.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SUI> uiList = new List<SUI>();

      foreach (DataRow row in dt.Rows)
      {
        SUI ui = new SUI();
        ui.GUID = Convert.ToString(row["GUID"]);
        ui.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        ui.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        ui.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        ui.BizPackageName = Convert.ToString(row["BizPackageName"]);
        ui.ProgramID = Convert.ToString(row["ProgramID"]);
        ui.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        ui.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        ui.Name = Convert.ToString(row["Name"]);
        ui.NameEnglish = Convert.ToString(row["NameEnglish"]);
        ui.UIType = Convert.ToString(row["UIType"]);
        ui.Description = Convert.ToString(row["Description"]);
        ui.SpecFile = Convert.ToString(row["SpecFile"]);
        
        ui.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        ui.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        ui.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        ui.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        ui.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        ui.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        ui.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        ui.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        ui.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        ui.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        uiList.Add(ui);
      }

      return uiList;
    }

    public DataTable SelectUIList(string uiName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT U.GUID " +
          "      , M.Name AS MicroserviceName" +
          "      , B.Name AS BizPackageName " +
          "      , U.ProgramID " +
          "      , U.DesignCompleteSkeleton " +
          "      , U.DesignCompleteDetail " +
          "	     , U.Name " +
          "      , U.UIType " +
          "	     , U.NameEnglish " +
          "      , (SELECT COUNT(*) FROM TB_EVENT WHERE UIGUID = U.GUID) AS EventCount " +
          "	     , U.Description " +
          "	     , U.RegisteredDate,    U.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    U.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , U.LastModifiedDate,  U.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  U.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_UI U " +
          "        LEFT OUTER JOIN TB_MICROSERVICE M ON U.MicroserviceGUID = M.GUID " +
          "        LEFT OUTER JOIN TB_BIZ_PACKAGE B ON U.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON U.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON U.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON U.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON U.LastModifiedUserGUID = U2.GUID " +
          "  WHERE U.Name LIKE @Name " +
          " ORDER BY M.Name, B.Name, U.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + uiName + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public SUI InsertUI(SBizPackage bp, string name = "")
    {
      SUI ui = new SUI();
      ui.GUID = Guid.NewGuid().ToString();
      ui.MicroserviceGUID = bp.MicroserviceGUID;
      ui.MicroserviceName = bp.MicroserviceName;
      ui.BizPackageGUID = bp.GUID;
      ui.BizPackageName = bp.Name;
      ui.Name = "New UI";
      SCommon.SetDateDesigner(ui, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_UI VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @UIType " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ui.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", ui.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", ui.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", ui.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", ui.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", ui.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", ui.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", ui.NameEnglish);
        cmd.Parameters.AddWithValue("@UIType", ui.UIType);
        cmd.Parameters.AddWithValue("@Description", ui.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ui.SpecFile);
        this.SetRegistered(cmd, ui);
        this.SetLastModified(cmd, ui);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return ui;
    }

    public void UpdateUI(SUI ui)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_UI " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , UIType = @UIType " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", ui.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", ui.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", ui.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", ui.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", ui.NameEnglish);
        cmd.Parameters.AddWithValue("@UIType", ui.UIType);
        cmd.Parameters.AddWithValue("@Description", ui.Description);
        cmd.Parameters.AddWithValue("@SpecFile", ui.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", ui.GUID);
        this.SetLastModified(cmd, ui);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUILastModifiedTemp(SUI ui)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_UI " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ui.GUID);
        this.SetLastModified(cmd, ui);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUIParent(SBizPackage bp, SUI ui)
    {
      ui.MicroserviceGUID = bp.MicroserviceGUID;
      ui.MicroserviceName = bp.MicroserviceName;
      ui.BizPackageGUID = bp.GUID;
      ui.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_UI " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", ui.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", ui.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", ui.GUID);
        this.SetLastModified(cmd, ui);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteUI(SUI ui)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_UI WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", ui.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Event

    public List<SEvent> SelectEventList(string uiGUID, string calleeAPIGUID)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT E.GUID " +
          "      , E.UIGUID " +
          "      , E.ProgramID " +
          "	     , E.Name " +
          "	     , E.CalleeApiGuid " +
          "	  FROM TB_EVENT E ";

        if(string.IsNullOrEmpty(uiGUID) == false)
          sql += " WHERE E.UIGUID = @UIGUID ";
        else if (string.IsNullOrEmpty(calleeAPIGUID) == false)
          sql += " WHERE E.CalleeApiGuid = @CalleeApiGuid ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(uiGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@UIGUID", uiGUID);
        else if (string.IsNullOrEmpty(calleeAPIGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@CalleeApiGuid", calleeAPIGUID);

        adapter.Fill(dt);
      }

      List<SEvent> evtList = new List<SEvent>();

      foreach (DataRow row in dt.Rows)
      {
        SEvent evt = new SEvent();
        evt.GUID = Convert.ToString(row["GUID"]);
        evt.UIGUID = Convert.ToString(row["UIGUID"]);
        evt.ProgramID = Convert.ToString(row["ProgramID"]);
        evt.Name = Convert.ToString(row["Name"]);
        evt.CalleeApiGuid = Convert.ToString(row["CalleeApiGuid"]);
        evtList.Add(evt);
      }

      return evtList;
    }

    public void InsertEvent(SEvent evt)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_EVENT VALUES " +
          " (  @GUID " +
          "  , @UIGUID " +
          "  , @ProgramID " +
          "  , @Name " +
          "  , @CalleeApiGuid " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", evt.GUID);
        cmd.Parameters.AddWithValue("@UIGUID", evt.UIGUID);
        cmd.Parameters.AddWithValue("@ProgramID", evt.ProgramID);
        cmd.Parameters.AddWithValue("@Name", evt.Name);
        cmd.Parameters.AddWithValue("@CalleeApiGuid", evt.CalleeApiGuid);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteEventList(string uiGUID)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_EVENT WHERE UIGUID = @UIGUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@UIGUID", uiGUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Job

    public List<SJob> SelectJobList(string bpGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT J.GUID " +
          "      , J.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , J.BizPackageGUID, B.Name AS BizPackageName " +
          "      , J.ProgramID " +
          "      , J.DesignCompleteSkeleton " +
          "      , J.DesignCompleteDetail " +
          "	     , J.Name " +
          "	     , J.NameEnglish " +
          "      , J.Schedule " +
          "      , J.Start " +
          "	     , J.Description " +
          "	     , J.SpecFile" +
          "	     , J.RegisteredDate,    J.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    J.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , J.LastModifiedDate,  J.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  J.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_JOB J " +
          "        INNER JOIN TB_MICROSERVICE M ON J.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON J.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON J.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON J.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON J.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON J.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(bpGUID) == false)
          sql += " WHERE J.BizPackageGUID = @BizPackageGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE J.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY J.Name ";
        else
          sql += " ORDER BY J.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(bpGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@BizPackageGUID", bpGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SJob> jobList = new List<SJob>();

      foreach (DataRow row in dt.Rows)
      {
        SJob job = new SJob();
        job.GUID = Convert.ToString(row["GUID"]);
        job.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        job.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        job.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        job.BizPackageName = Convert.ToString(row["BizPackageName"]);
        job.ProgramID = Convert.ToString(row["ProgramID"]);
        job.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        job.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        job.Name = Convert.ToString(row["Name"]);
        job.NameEnglish = Convert.ToString(row["NameEnglish"]);
        job.Schedule = Convert.ToString(row["Schedule"]);
        job.Start = Convert.ToString(row["Start"]);
        job.Description = Convert.ToString(row["Description"]);
        job.SpecFile = Convert.ToString(row["SpecFile"]);
        
        job.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        job.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        job.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        job.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        job.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        job.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        job.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        job.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        job.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        job.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        jobList.Add(job);
      }

      return jobList;
    }

    public DataTable SelectJobList(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT J.GUID " +
          "      , M.Name AS MicroserviceName" +
          "      , B.Name AS BizPackageName " +
          "      , J.ProgramID " +
          "      , J.DesignCompleteSkeleton " +
          "      , J.DesignCompleteDetail " +
          "	     , J.Name " +
          "	     , J.NameEnglish " +
          "      , J.Schedule " +
          "      , J.Start " +
          "      , (SELECT COUNT(*) FROM TB_STEP WHERE JobGUID = J.GUID) AS StepCount " + 
          "	     , J.Description " +
          "	     , J.RegisteredDate,    J.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    J.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , J.LastModifiedDate,  J.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  J.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_JOB J " +
          "        INNER JOIN TB_MICROSERVICE M ON J.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON J.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_PART P1 ON J.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON J.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON J.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON J.LastModifiedUserGUID = U2.GUID " +
          "  WHERE J.Name LIKE @Name " +
          " ORDER BY M.Name, B.Name, J.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
        adapter.Fill(dt);
      }

      return dt;
    }

    public SJob InsertJob(SBizPackage bp, string name = "")
    {
      SJob job = new SJob();
      job.GUID = Guid.NewGuid().ToString();
      job.MicroserviceGUID = bp.MicroserviceGUID;
      job.MicroserviceName = bp.MicroserviceName;
      job.BizPackageGUID = bp.GUID;
      job.BizPackageName = bp.Name;
      job.Name = name.Length == 0 ? "New Job" : name;
      SCommon.SetDateDesigner(job, true);

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_JOB VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Schedule " +
          "  , @Start " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", job.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", job.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", job.BizPackageGUID);
        cmd.Parameters.AddWithValue("@ProgramID", job.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", job.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", job.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", job.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", job.NameEnglish);
        cmd.Parameters.AddWithValue("@Schedule", job.Schedule);
        cmd.Parameters.AddWithValue("@Start", job.Start);
        cmd.Parameters.AddWithValue("@Description", job.Description);
        cmd.Parameters.AddWithValue("@SpecFile", job.SpecFile);
        this.SetRegistered(cmd, job);
        this.SetLastModified(cmd, job);
        cmd.ExecuteNonQuery();

        connection.Close();
      }

      return job;
    }

    public void UpdateJob(SJob job)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_JOB " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , Schedule = @Schedule " +
          "      , Start = @Start " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", job.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", job.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", job.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", job.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", job.NameEnglish);
        cmd.Parameters.AddWithValue("@Schedule", job.Schedule);
        cmd.Parameters.AddWithValue("@Start", job.Start);
        cmd.Parameters.AddWithValue("@Description", job.Description);
        cmd.Parameters.AddWithValue("@SpecFile", job.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", job.GUID);
        this.SetLastModified(cmd, job);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateJobLastModifiedTemp(SJob job)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_JOB " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", job.GUID);
        this.SetLastModified(cmd, job);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateJobParent(SBizPackage bp, SJob job)
    {
      job.MicroserviceGUID = bp.MicroserviceGUID;
      job.MicroserviceName = bp.MicroserviceName;
      job.BizPackageGUID = bp.GUID;
      job.BizPackageName = bp.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_JOB " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", job.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", job.BizPackageGUID);
        cmd.Parameters.AddWithValue("@GUID", job.GUID);
        this.SetLastModified(cmd, job);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteJob(SJob job)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_JOB WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", job.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Step

    public List<SStep> SelectStepList(string jobGUID, string guid, bool sortByName = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT S.GUID " +
          "      , S.MicroserviceGUID, M.Name AS MicroserviceName" +
          "      , S.BizPackageGUID, B.Name AS BizPackageName " +
          "      , S.JobGUID, J.Name AS JobName " +
          "      , S.ProgramID " +
          "      , S.DesignCompleteSkeleton " +
          "      , S.DesignCompleteDetail " +
          "	     , S.Name " +
          "	     , S.NameEnglish " +
          "	     , S.Description " +
          "	     , S.SpecFile" +
          "	     , S.RegisteredDate,    S.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    S.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "	     , S.LastModifiedDate,  S.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  S.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "	  FROM TB_STEP S " +
          "        INNER JOIN TB_MICROSERVICE M ON S.MicroserviceGUID = M.GUID " +
          "        INNER JOIN TB_BIZ_PACKAGE B ON S.BizPackageGUID = B.GUID " +
          "        INNER JOIN TB_JOB J ON S.JobGUID = J.GUID " +
          "        INNER JOIN TB_PART P1 ON S.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 ON S.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 ON S.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 ON S.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(jobGUID) == false)
          sql += " WHERE S.JobGUID = @JobGUID ";
        else if (string.IsNullOrEmpty(guid) == false)
          sql += " WHERE S.GUID = @GUID ";

        if (sortByName)
          sql += " ORDER BY S.Name ";
        else
          sql += " ORDER BY S.ProgramID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(jobGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@JobGUID", jobGUID);
        else if (string.IsNullOrEmpty(guid) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@GUID", guid);

        adapter.Fill(dt);
      }

      List<SStep> stepList = new List<SStep>();

      foreach (DataRow row in dt.Rows)
      {
        SStep step = new SStep();
        step.GUID = Convert.ToString(row["GUID"]);
        step.MicroserviceGUID = Convert.ToString(row["MicroserviceGUID"]);
        step.MicroserviceName = Convert.ToString(row["MicroserviceName"]);
        step.BizPackageGUID = Convert.ToString(row["BizPackageGUID"]);
        step.BizPackageName = Convert.ToString(row["BizPackageName"]);
        step.JobGUID = Convert.ToString(row["JobGUID"]);
        step.JobName = Convert.ToString(row["JobName"]);
        step.ProgramID = Convert.ToString(row["ProgramID"]);
        step.DesignCompleteSkeleton = SCommon.GetBoolean(row["DesignCompleteSkeleton"]);
        step.DesignCompleteDetail = SCommon.GetBoolean(row["DesignCompleteDetail"]);
        step.Name = Convert.ToString(row["Name"]);
        step.NameEnglish = Convert.ToString(row["NameEnglish"]);
        step.Description = Convert.ToString(row["Description"]);
        step.SpecFile = Convert.ToString(row["SpecFile"]);
        
        step.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        step.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        step.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        step.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        step.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);
        
        step.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        step.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        step.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        step.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        step.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);
        
        stepList.Add(step);
      }

      return stepList;
    }

    public void InsertStep(SStep step)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_STEP VALUES " +
          " (  @GUID " +
          "  , @MicroserviceGUID " +
          "  , @BizPackageGUID " +
          "  , @JobGUID " +
          "  , @ProgramID " +
          "  , @DesignCompleteSkeleton " +
          "  , @DesignCompleteDetail " +
          "  , @Name " +
          "  , @NameEnglish " +
          "  , @Description " +
          "  , @SpecFile " +
          "  , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", step.GUID);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", step.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", step.BizPackageGUID);
        cmd.Parameters.AddWithValue("@JobGUID", step.JobGUID);
        cmd.Parameters.AddWithValue("@ProgramID", step.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", step.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", step.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", step.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", step.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", step.Description);
        cmd.Parameters.AddWithValue("@SpecFile", step.SpecFile);
        this.SetRegistered(cmd, step);
        this.SetLastModified(cmd, step);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateStep(SStep step)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_STEP " +
          "    SET ProgramID = @ProgramID " +
          "      , DesignCompleteSkeleton = @DesignCompleteSkeleton" +
          "      , DesignCompleteDetail = @DesignCompleteDetail" +
          "      , Name = @Name " +
          "      , NameEnglish = @NameEnglish " +
          "      , Description = @Description " +
          "      , SpecFile = @SpecFile " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@ProgramID", step.ProgramID);
        cmd.Parameters.AddWithValue("@DesignCompleteSkeleton", step.DesignCompleteSkeleton);
        cmd.Parameters.AddWithValue("@DesignCompleteDetail", step.DesignCompleteDetail);
        cmd.Parameters.AddWithValue("@Name", step.Name);
        cmd.Parameters.AddWithValue("@NameEnglish", step.NameEnglish);
        cmd.Parameters.AddWithValue("@Description", step.Description);
        cmd.Parameters.AddWithValue("@SpecFile", step.SpecFile);
        cmd.Parameters.AddWithValue("@GUID", step.GUID);
        this.SetLastModified(cmd, step);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateStepLastModifiedTemp(SStep step)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_STEP " +
          "    SET LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", step.GUID);
        this.SetLastModified(cmd, step);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateStepParent(SJob job, SStep step)
    {
      step.MicroserviceGUID = job.MicroserviceGUID;
      step.MicroserviceName = job.MicroserviceName;
      step.BizPackageGUID = job.BizPackageGUID;
      step.BizPackageName = job.BizPackageName;
      step.JobGUID = job.GUID;
      step.JobName = job.Name;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_STEP " +
          "    SET MicroserviceGUID = @MicroserviceGUID " +
          "      , BizPackageGUID = @BizPackageGUID " +
          "      , JobGUID = @JobGUID " +
          "      , LastModifiedDate = @LastModifiedDate, LastModifiedPartGUID = @LastModifiedPartGUID, LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MicroserviceGUID", step.MicroserviceGUID);
        cmd.Parameters.AddWithValue("@BizPackageGUID", step.BizPackageGUID);
        cmd.Parameters.AddWithValue("@JobGUID", step.JobGUID);
        cmd.Parameters.AddWithValue("@GUID", step.GUID);
        this.SetLastModified(cmd, step);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteStep(SStep step)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_STEP WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", step.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Program

    public DataTable SelectProgramList(string name, string partName, string userName)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT X.GUID, M.Name AS MicroserviceName, B.Name AS BizPackageName, X.Type, X.ProgramID, X.Name, X.NameEnglish, X.DesignCompleteSkeleton, X.DesignCompleteDetail, X.Description " +
          "      , X.RegisteredDate,    X.RegisteredPartGUID,   P1.Name AS RegisteredPartName,    X.RegisteredUserGUID,   U1.Name AS RegisteredUserName " +
          "      , X.LastModifiedDate,  X.LastModifiedPartGUID, P2.Name AS LastModifiedPartName,  X.LastModifiedUserGUID, U2.Name AS LastModifiedUserName " +
          "   FROM( " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'API' AS Type,        ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_API " +
          "        UNION " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'Publisher' AS Type,  ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_PUBLISHER " +
          "        UNION " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'Subscriber' AS Type, ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_SUBSCRIBER " +
          "        UNION " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'Other' AS Type,      ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_OTHER " +
          "        UNION " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'UI' AS Type,         ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_UI " +
          "        UNION " +
          "        SELECT GUID, MicroserviceGUID, BizPackageGUID, 'Batch' AS Type,      ProgramID, Name, NameEnglish, DesignCompleteSkeleton, DesignCompleteDetail, Description, RegisteredDate, RegisteredPartGUID, RegisteredUserGUID, LastModifiedDate, LastModifiedPartGUID, LastModifiedUserGUID " +
          "          FROM TB_JOB " +
          "       ) X " +
          "         INNER JOIN TB_MICROSERVICE M ON X.MicroserviceGUID = M.GUID " +
          "         INNER JOIN TB_BIZ_PACKAGE B ON X.BizPackageGUID = B.GUID " +
          "         INNER JOIN TB_PART P1 ON X.RegisteredPartGUID = P1.GUID " +
          "         INNER JOIN TB_PART P2 ON X.LastModifiedPartGUID = P2.GUID " +
          "         INNER JOIN TB_USER U1 ON X.RegisteredUserGUID = U1.GUID " +
          "         INNER JOIN TB_USER U2 ON X.LastModifiedUserGUID = U2.GUID " +
          "   WHERE X.Name LIKE @Name ";

        if (string.IsNullOrEmpty(partName) == false)
          sql += " AND (RegisteredPartName = @PartName OR LastModifiedPartName = @PartName) ";

        if (string.IsNullOrEmpty(userName) == false)
          sql += " AND (RegisteredUserName = @UserName OR LastModifiedUserName = @UserName) ";
        
        sql += " ORDER BY X.Name ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");

        if (string.IsNullOrEmpty(partName) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@PartName", partName);

        if (string.IsNullOrEmpty(userName) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@UserName", userName);

        adapter.Fill(dt);
      }

      return dt;
    }

    public DataTable SelectComponentCountListByDate(string fromYMD, string toYMD)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT Type, LastModifiedDate, Cnt " +
          "  FROM( " +
          "    SELECT 'API' AS Type,        LastModifiedDate, COUNT(*) AS Cnt FROM TB_API         GROUP BY LastModifiedDate " +
          "    UNION " +
          "    SELECT 'Publisher' AS Type,  LastModifiedDate, COUNT(*) AS Cnt FROM TB_PUBLISHER   GROUP BY LastModifiedDate " +
          "    UNION " +
          "    SELECT 'Subscriber' AS Type, LastModifiedDate, COUNT(*) AS Cnt FROM TB_SUBSCRIBER  GROUP BY LastModifiedDate " +
          "    UNION " +
          "    SELECT 'Other' AS Type,      LastModifiedDate, COUNT(*) AS Cnt FROM TB_OTHER       GROUP BY LastModifiedDate " +
          "       ) " +
          " WHERE LastModifiedDate BETWEEN @FROM_DT AND @TO_DT " +
          " ORDER BY LastModifiedDate ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@FROM_DT", fromYMD);
        adapter.SelectCommand.Parameters.AddWithValue("@TO_DT", toYMD);
        adapter.Fill(dt);
      }

      return dt;
    }

    //public DataTable SelectComponentCountListByDate(string msGUID, string fromYMD, string toYMD)
    //{
    //  DataTable dt = new DataTable();

    //  using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
    //  {
    //    string sql =
    //      " SELECT MicroserviceGUID, Type, LastModifiedDate, Cnt " +
    //      "  FROM( " +
    //      "    SELECT MicroserviceGUID, 'API' AS Type,        LastModifiedDate, COUNT(*) AS Cnt FROM TB_API         GROUP BY MicroserviceGUID, Type, LastModifiedDate " +
    //      "    UNION " +
    //      "    SELECT MicroserviceGUID, 'Publisher' AS Type,  LastModifiedDate, COUNT(*) AS Cnt FROM TB_PUBLISHER   GROUP BY MicroserviceGUID, Type, LastModifiedDate " +
    //      "    UNION " +
    //      "    SELECT MicroserviceGUID, 'Subscriber' AS Type, LastModifiedDate, COUNT(*) AS Cnt FROM TB_SUBSCRIBER  GROUP BY MicroserviceGUID, Type, LastModifiedDate " +
    //      "    UNION " +
    //      "    SELECT MicroserviceGUID, 'Other' AS Type,      LastModifiedDate, COUNT(*) AS Cnt FROM TB_OTHER       GROUP BY MicroserviceGUID, Type, LastModifiedDate " +
    //      "       ) " +
    //      " WHERE MicroserviceGUID != '' " +
    //      "   AND LastModifiedDate BETWEEN @FROM_DT AND @TO_DT ";

    //    if (string.IsNullOrEmpty(msGUID) == false)
    //      sql += " AND MicroserviceGUID = @MicroserviceGUID ";

    //    sql += " ORDER BY LastModifiedDate ";

    //    SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
    //    adapter.SelectCommand.Parameters.AddWithValue("@FROM_DT", fromYMD);
    //    adapter.SelectCommand.Parameters.AddWithValue("@TO_DT", toYMD);

    //    if (string.IsNullOrEmpty(msGUID) == false)
    //      adapter.SelectCommand.Parameters.AddWithValue("@MicroserviceGUID", msGUID);

    //    adapter.Fill(dt);
    //  }

    //  return dt;
    //}

    #endregion

    #region Word

    public List<SWord> SelectWordList(string type, bool sortByKorean = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT Korean " +
          "      , English " +
          "	  FROM TB_WORD " +
          "  WHERE Type = @Type ";

        if (sortByKorean)
          sql += " ORDER BY Korean ";
        else
          sql += " ORDER BY English ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Type", type);
        adapter.Fill(dt);
      }

      List<SWord> wordList = new List<SWord>();

      foreach (DataRow row in dt.Rows)
      {
        SWord word = new SWord();
        word.Korean = Convert.ToString(row["Korean"]);
        word.English = Convert.ToString(row["English"]);
        wordList.Add(word);
      }

      return wordList;
    }

    public DataTable SelectWordListDataTable(string type, string korean, string english, bool sortByKorean = true)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT Korean " +
          "      , LOWER(English) AS English " +
          "   FROM TB_WORD " +
          "  WHERE Type = @Type ";

        if (korean.Length > 0)
          sql += " AND Korean LIKE @Korean ";

        if (english.Length > 0)
          sql += " AND English LIKE @English ";

        if (sortByKorean)
          sql += " ORDER BY Korean ";
        else
          sql += " ORDER BY English ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Type", type);

        if (korean.Length > 0)
          adapter.SelectCommand.Parameters.AddWithValue("@Korean", "%" + korean + "%");

        if (english.Length > 0)
          adapter.SelectCommand.Parameters.AddWithValue("@English", "%" + english + "%");

        adapter.Fill(dt);
      }

      return dt;
    }

    public DataRow SelectWord(string type, string korean)
    {
      DataTable dt = new DataTable();
      DataRow row = null;

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT Korean " +
          "      , LOWER(English) AS English " +
          "   FROM TB_WORD " +
          "  WHERE Type = @Type " + 
          "    AND Korean = @Korean ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Type", type);
        adapter.SelectCommand.Parameters.AddWithValue("@Korean", korean);

        adapter.Fill(dt);
      }

      if (dt.Rows.Count == 1)
        row = dt.Rows[0];

      return row;
    }


    public void InsertWord(string type, string korean, string english)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_WORD VALUES " +
          " (  @Type " +
          "  , @Korean " +
          "  , @English " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Type", type);
        cmd.Parameters.AddWithValue("@Korean", korean);
        cmd.Parameters.AddWithValue("@English", english.ToLower());
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateWord(SWord word)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_WORD " +
          "    SET English = @English " +
          "  WHERE Type = @Type " +
          "    AND Korean = @Korean ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@English", word.English.ToLower());
        cmd.Parameters.AddWithValue("@Type", word.Type);
        cmd.Parameters.AddWithValue("@Korean", word.Korean);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteWord(SWord word)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_WORD WHERE Type = @Type AND Korean = @Korean ";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Type", word.Type);
        cmd.Parameters.AddWithValue("@Korean", word.Korean);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Project Option

    public SProject SelectProject()
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql = "SELECT * FROM TB_PROJECT WHERE GUID = 'project'";
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      DataRow row = dt.Rows[0];
      SProject project = new SProject();
      project.GUID = Convert.ToString(row["GUID"]);
      project.Name = Convert.ToString(row["Name"]);
      project.LogoURL = Convert.ToString(row["LogoURL"]);
      project.Contact = Convert.ToString(row["Contact"]);
      project.ProjectFolder = Convert.ToString(row["ProjectFolder"]);
      project.DeployFolder = Convert.ToString(row["DeployFolder"]);
      project.Dictionary = SCommon.GetBoolean(row["Dictionary"]);

      project.SampleMSCode = Convert.ToString(row["SampleMSCode"]);

      project.SampleBPCode = Convert.ToString(row["SampleBPCode"]);
      project.SampleBPSourcePackage = Convert.ToString(row["SampleBPSourcePackage"]);

      project.SampleControllerName = Convert.ToString(row["SampleControllerName"]);
      project.SampleControllerClass = Convert.ToString(row["SampleControllerClass"]);
      project.SampleControllerURI = Convert.ToString(row["SampleControllerURI"]);

      project.SampleAPIName = Convert.ToString(row["SampleAPIName"]);
      project.SampleAPIMethod = Convert.ToString(row["SampleAPIMethod"]);
      project.SampleAPIURL = Convert.ToString(row["SampleAPIURL"]);
      project.SampleAPIInput = Convert.ToString(row["SampleAPIInput"]);
      project.SampleAPIOutput = Convert.ToString(row["SampleAPIOutput"]);

      project.SampleProducerName = Convert.ToString(row["SampleProducerName"]);
      project.SampleProducerClass = Convert.ToString(row["SampleProducerClass"]);

      project.SamplePubName = Convert.ToString(row["SamplePubName"]);
      project.SamplePubMethod = Convert.ToString(row["SamplePubMethod"]);
      project.SamplePubInput = Convert.ToString(row["SamplePubInput"]);
      project.SamplePubTopic = Convert.ToString(row["SamplePubTopic"]);

      project.SampleConsumerName = Convert.ToString(row["SampleConsumerName"]);
      project.SampleConsumerClass = Convert.ToString(row["SampleConsumerClass"]);

      project.SampleSubName = Convert.ToString(row["SampleSubName"]);
      project.SampleSubMethod = Convert.ToString(row["SampleSubMethod"]);
      project.SampleSubInput = Convert.ToString(row["SampleSubInput"]);

      project.SampleDtoName = Convert.ToString(row["SampleDtoName"]);
      project.SampleDtoClass = Convert.ToString(row["SampleDtoClass"]);

      project.SampleEntityName = Convert.ToString(row["SampleEntityName"]);
      project.SampleEntityClass = Convert.ToString(row["SampleEntityClass"]);
      project.SampleEntityTable = Convert.ToString(row["SampleEntityTable"]);

      project.SampleBRName = Convert.ToString(row["SampleBRName"]);
      project.SampleBRClass = Convert.ToString(row["SampleBRClass"]);
      project.SampleBROpName = Convert.ToString(row["SampleBROpName"]);
      project.SampleBROpMethod = Convert.ToString(row["SampleBROpMethod"]);

      project.SampleDAName = Convert.ToString(row["SampleDAName"]);
      project.SampleDAClass = Convert.ToString(row["SampleDAClass"]);
      project.SampleDAOpName = Convert.ToString(row["SampleDAOpName"]);
      project.SampleDAOpMethod = Convert.ToString(row["SampleDAOpMethod"]);

      project.SampleUIName = Convert.ToString(row["SampleUIName"]);
      project.SampleUIProgram = Convert.ToString(row["SampleUIProgram"]);

      project.SampleJobSchedule = Convert.ToString(row["SampleJobSchedule"]);
      project.SampleJobStart = Convert.ToString(row["SampleJobStart"]);

      project.AddProducer = SCommon.GetBoolean(row["AddProducer"]);
      project.AddPublisher = SCommon.GetBoolean(row["AddPublisher"]);
      project.AddConsumer = SCommon.GetBoolean(row["AddConsumer"]);
      project.AddSubscriber = SCommon.GetBoolean(row["AddSubscriber"]);
      project.GenerateSpec = SCommon.GetBoolean(row["GenerateSpec"]);
      project.GenerateCode = SCommon.GetBoolean(row["GenerateCode"]);

      project.CodeGenClassNameDA = Convert.ToString(row["CodeGenClassNameDA"]);
      project.CodeGenClassNameJDA = Convert.ToString(row["CodeGenClassNameJDA"]);
      project.CodeGenSQLWithDA = SCommon.GetBoolean(row["CodeGenSQLWithDA"]);

      return project;
    }

    public void UpdateProject(SProject project)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          "UPDATE TB_PROJECT SET " +
          "   Name = @Name " +
          " , LogoURL = @LogoURL " +
          " , Contact = @Contact " +
          " , ProjectFolder = @ProjectFolder " +
          " , DeployFolder = @DeployFolder " +
          " , Dictionary = @Dictionary " +
          
          " , SampleMSCode = @SampleMSCode " +
          
          " , SampleBPCode = @SampleBPCode " +
          " , SampleBPSourcePackage = @SampleBPSourcePackage " +
          
          " , SampleControllerName = @SampleControllerName " +
          " , SampleControllerClass = @SampleControllerClass " +
          " , SampleControllerURI = @SampleControllerURI " +
          
          " , SampleAPIName = @SampleAPIName " +
          " , SampleAPIMethod = @SampleAPIMethod " +
          " , SampleAPIURL = @SampleAPIURL " +
          " , SampleAPIInput = @SampleAPIInput " +
          " , SampleAPIOutput = @SampleAPIOutput " +
          
          " , SampleProducerName = @SampleProducerName " +
          " , SampleProducerClass = @SampleProducerClass " +
          
          " , SamplePubName = @SamplePubName " +
          " , SamplePubMethod = @SamplePubMethod " +
          " , SamplePubInput = @SamplePubInput " +
          " , SamplePubTopic = @SamplePubTopic " +
          
          " , SampleConsumerName = @SampleConsumerName " +
          " , SampleConsumerClass = @SampleConsumerClass " +
          
          " , SampleSubName = @SampleSubName " +
          " , SampleSubMethod = @SampleSubMethod " +
          " , SampleSubInput = @SampleSubInput " +
          
          " , SampleDtoName = @SampleDtoName " +
          " , SampleDtoClass = @SampleDtoClass " +
          
          " , SampleEntityName = @SampleEntityName " +
          " , SampleEntityClass = @SampleEntityClass " +
          " , SampleEntityTable = @SampleEntityTable " +
          
          " , SampleBRName = @SampleBRName " +
          " , SampleBRClass = @SampleBRClass " +
          " , SampleBROpName = @SampleBROpName " +
          " , SampleBROpMethod = @SampleBROpMethod " +
          
          " , SampleDAName = @SampleDAName " +
          " , SampleDAClass = @SampleDAClass " +
          " , SampleDAOpName = @SampleDAOpName " +
          " , SampleDAOpMethod = @SampleDAOpMethod " +
          
          " , SampleUIName = @SampleUIName " +
          " , SampleUIProgram = @SampleUIProgram " +
          
          " , SampleJobSchedule = @SampleJobSchedule " +
          " , SampleJobStart = @SampleJobStart " +
          
          " , AddProducer = @AddProducer " +
          " , AddPublisher = @AddPublisher " +
          " , AddConsumer = @AddConsumer " +
          " , AddSubscriber = @AddSubscriber " +
          " , GenerateSpec = @GenerateSpec " +
          " , GenerateCode = @GenerateCode " +
          
          " , CodeGenClassNameDA = @CodeGenClassNameDA " +
          " , CodeGenClassNameJDA = @CodeGenClassNameJDA " +
          " , CodeGenSQLWithDA = @CodeGenSQLWithDA " +

          " WHERE GUID = @GUID";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Name", project.Name);
        cmd.Parameters.AddWithValue("@LogoURL", project.LogoURL);
        cmd.Parameters.AddWithValue("@Contact", project.Contact);
        cmd.Parameters.AddWithValue("@ProjectFolder", project.ProjectFolder);
        cmd.Parameters.AddWithValue("@DeployFolder", project.DeployFolder);
        cmd.Parameters.AddWithValue("@Dictionary", project.Dictionary);
        
        cmd.Parameters.AddWithValue("@SampleMSCode", project.SampleMSCode);
        
        cmd.Parameters.AddWithValue("@SampleBPCode", project.SampleBPCode);
        cmd.Parameters.AddWithValue("@SampleBPSourcePackage", project.SampleBPSourcePackage);
        
        cmd.Parameters.AddWithValue("@SampleControllerName", project.SampleControllerName);
        cmd.Parameters.AddWithValue("@SampleControllerClass", project.SampleControllerClass);
        cmd.Parameters.AddWithValue("@SampleControllerURI", project.SampleControllerURI);
        
        cmd.Parameters.AddWithValue("@SampleAPIName", project.SampleAPIName);
        cmd.Parameters.AddWithValue("@SampleAPIMethod", project.SampleAPIMethod);
        cmd.Parameters.AddWithValue("@SampleAPIURL", project.SampleAPIURL);
        cmd.Parameters.AddWithValue("@SampleAPIInput", project.SampleAPIInput);
        cmd.Parameters.AddWithValue("@SampleAPIOutput", project.SampleAPIOutput);
        
        cmd.Parameters.AddWithValue("@SampleProducerName", project.SampleProducerName);
        cmd.Parameters.AddWithValue("@SampleProducerClass", project.SampleProducerClass);
        
        cmd.Parameters.AddWithValue("@SamplePubName", project.SamplePubName);
        cmd.Parameters.AddWithValue("@SamplePubMethod", project.SamplePubMethod);
        cmd.Parameters.AddWithValue("@SamplePubInput", project.SamplePubInput);
        cmd.Parameters.AddWithValue("@SamplePubTopic", project.SamplePubTopic);
        
        cmd.Parameters.AddWithValue("@SampleConsumerName", project.SampleConsumerName);
        cmd.Parameters.AddWithValue("@SampleConsumerClass", project.SampleConsumerClass);
        
        cmd.Parameters.AddWithValue("@SampleSubName", project.SampleSubName);
        cmd.Parameters.AddWithValue("@SampleSubMethod", project.SampleSubMethod);
        cmd.Parameters.AddWithValue("@SampleSubInput", project.SampleSubInput);
        
        cmd.Parameters.AddWithValue("@SampleDtoName", project.SampleDtoName);
        cmd.Parameters.AddWithValue("@SampleDtoClass", project.SampleDtoClass);
        
        cmd.Parameters.AddWithValue("@SampleEntityName", project.SampleEntityName);
        cmd.Parameters.AddWithValue("@SampleEntityClass", project.SampleEntityClass);
        cmd.Parameters.AddWithValue("@SampleEntityTable", project.SampleEntityTable);
        
        cmd.Parameters.AddWithValue("@SampleBRName", project.SampleBRName);
        cmd.Parameters.AddWithValue("@SampleBRClass", project.SampleBRClass);
        cmd.Parameters.AddWithValue("@SampleBROpName", project.SampleBROpName);
        cmd.Parameters.AddWithValue("@SampleBROpMethod", project.SampleBROpMethod);
        
        cmd.Parameters.AddWithValue("@SampleDAName", project.SampleDAName);
        cmd.Parameters.AddWithValue("@SampleDAClass", project.SampleDAClass);
        cmd.Parameters.AddWithValue("@SampleDAOpName", project.SampleDAOpName);
        cmd.Parameters.AddWithValue("@SampleDAOpMethod", project.SampleDAOpMethod);
        
        cmd.Parameters.AddWithValue("@SampleUIName", project.SampleUIName);
        cmd.Parameters.AddWithValue("@SampleUIProgram", project.SampleUIProgram);
        
        cmd.Parameters.AddWithValue("@SampleJobSchedule", project.SampleJobSchedule);
        cmd.Parameters.AddWithValue("@SampleJobStart", project.SampleJobStart);
        
        cmd.Parameters.AddWithValue("@AddProducer", project.AddProducer);
        cmd.Parameters.AddWithValue("@AddPublisher", project.AddPublisher);
        cmd.Parameters.AddWithValue("@AddConsumer", project.AddConsumer);
        cmd.Parameters.AddWithValue("@AddSubscriber", project.AddSubscriber);
        cmd.Parameters.AddWithValue("@GenerateSpec", project.GenerateSpec);
        cmd.Parameters.AddWithValue("@GenerateCode", project.GenerateCode);
        
        cmd.Parameters.AddWithValue("@CodeGenClassNameDA", project.CodeGenClassNameDA);
        cmd.Parameters.AddWithValue("@CodeGenClassNameJDA", project.CodeGenClassNameJDA);
        cmd.Parameters.AddWithValue("@CodeGenSQLWithDA", project.CodeGenSQLWithDA);

        cmd.Parameters.AddWithValue("@GUID", project.GUID);

        
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region Part

    public List<SPart> SelectPartList()
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          "SELECT " + 
          "   P.GUID " +
          "	, P.Name " +
          "	, P.Description " +

          "	, P.RegisteredDate " +
          "	, P.RegisteredPartGUID " +
          "	, P1.Name as RegisteredPartName " +
          "	, P.RegisteredUserGUID " +
          "	, U1.Name as RegisteredUserName " +

          "	, P.LastModifiedDate " +
          "	, P.LastModifiedPartGUID " +
          "	, P2.Name as LastModifiedPartName " +
          "	, P.LastModifiedUserGUID " +
          "	, U2.Name as LastModifiedUserName " +

          "	FROM TB_PART P " +
          "      INNER JOIN TB_PART P1 on P.RegisteredPartGUID = P1.GUID " +
          "      INNER JOIN TB_PART P2 on P.LastModifiedPartGUID = P2.GUID " +
          "      INNER JOIN TB_USER U1 on P.RegisteredUserGUID = U1.GUID " +
          "      INNER JOIN TB_USER U2 on P.LastModifiedUserGUID = U2.GUID ";

        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection);
        adapter.Fill(dt);
      }

      List<SPart> partList = new List<SPart>();

      foreach (DataRow row in dt.Rows)
      {
        SPart part = new SPart();
        part.GUID = Convert.ToString(row["GUID"]);
        part.Name = Convert.ToString(row["Name"]);
        part.Description = Convert.ToString(row["Description"]);

        part.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        part.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        part.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        part.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        part.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        part.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        part.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        part.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        part.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        part.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        partList.Add(part);
      }

      return partList;
    }

    public SPart SelectPartByName(string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          "SELECT " +
          "   P.GUID " +
          "	, P.Name " +
          "	, P.Description " +

          "	, P.RegisteredDate " +
          "	, P.RegisteredPartGUID " +
          "	, P1.Name as RegisteredPartName " +
          "	, P.RegisteredUserGUID " +
          "	, U1.Name as RegisteredUserName " +

          "	, P.LastModifiedDate " +
          "	, P.LastModifiedPartGUID " +
          "	, P2.Name as LastModifiedPartName " +
          "	, P.LastModifiedUserGUID " +
          "	, U2.Name as LastModifiedUserName " +

          "	FROM TB_PART P " +
          "      INNER JOIN TB_PART P1 on P.RegisteredPartGUID = P1.GUID " +
          "      INNER JOIN TB_PART P2 on P.LastModifiedPartGUID = P2.GUID " +
          "      INNER JOIN TB_USER U1 on P.RegisteredUserGUID = U1.GUID " +
          "      INNER JOIN TB_USER U2 on P.LastModifiedUserGUID = U2.GUID " +

          " WHERE P.Name = @Name";

        var adapter = new SQLiteDataAdapter(sql, connection);
        adapter.SelectCommand.Parameters.AddWithValue("@Name", name);
        adapter.Fill(dt);
      }

      List<SPart> partList = new List<SPart>();

      foreach (DataRow row in dt.Rows)
      {
        SPart part = new SPart();
        part.GUID = Convert.ToString(row["GUID"]);
        part.Name = Convert.ToString(row["Name"]);
        part.Description = Convert.ToString(row["Description"]);

        part.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        part.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        part.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        part.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        part.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        part.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        part.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        part.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        part.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        part.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        partList.Add(part);
      }

      return partList[0];
    }

    public void InsertPart(SPart part)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = 
          "INSERT INTO TB_PART VALUES ( " + 
          "  @GUID, @Name, @Description, " + 
          "  @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " + 
          " )";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", part.GUID);
        cmd.Parameters.AddWithValue("@Name", part.Name);
        cmd.Parameters.AddWithValue("@Description", part.Description);
        this.SetRegistered(cmd);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdatePart(SPart part)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = 
          "UPDATE TB_PART SET " + 
          "   Name = @Name " +
          " , Description = @Description " + 
          " , LastModifiedDate = @LastModifiedDate " + 
          " , LastModifiedPartGUID = @LastModifiedPartGUID " + 
          " , LastModifiedUserGUID = @LastModifiedUserGUID " + 
          " WHERE GUID = @GUID";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Name", part.Name);
        cmd.Parameters.AddWithValue("@Description", part.Description);
        cmd.Parameters.AddWithValue("@GUID", part.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeletePart(SPart part)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_PART WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", part.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion

    #region User

    public List<SUser> SelectUserList(string partGUID, string name)
    {
      DataTable dt = new DataTable();

      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        string sql =
          " SELECT U.GUID " +
          "      , U.PartGUID, P0.Name as PartName " +
          "	     , U.Name " +
          "	     , U.Password " +
          "	     , U.Role " +
          "	     , U.ViewOnly " +
          "	     , U.Description " +
          "	     , U.Width " +
          "	     , U.Height " +
          "	     , U.SplitterDistance " +
          "	     , U.TreeFont " +
          "	     , U.TreeFontSize " +
          "	     , U.ExpandType " +
          "	     , U.ShowID " +
          "	     , U.ShowEng " +
          "	     , U.SortType " +
          "	     , U.MonospacedFontDAOperationDesc " +
          "	     , U.RegisteredDate,   U.RegisteredPartGUID,   P1.Name as RegisteredPartName,    U.RegisteredUserGUID,   U1.Name as RegisteredUserName " +
          "	     , U.LastModifiedDate, U.LastModifiedPartGUID, P2.Name as LastModifiedPartName,  U.LastModifiedUserGUID, U2.Name as LastModifiedUserName " +
          "	  FROM TB_USER U " +
          "        INNER JOIN TB_PART P0 on U.PartGUID = P0.GUID " +
          "        INNER JOIN TB_PART P1 on U.RegisteredPartGUID = P1.GUID " +
          "        INNER JOIN TB_PART P2 on U.LastModifiedPartGUID = P2.GUID " +
          "        INNER JOIN TB_USER U1 on U.RegisteredUserGUID = U1.GUID " +
          "        INNER JOIN TB_USER U2 on U.LastModifiedUserGUID = U2.GUID ";

        if(string.IsNullOrEmpty(partGUID) == false)
          sql += " WHERE U.PartGUID = @PartGUID ";

        if (string.IsNullOrEmpty(name) == false)
          sql += " AND U.Name = @Name ";

        sql += " ORDER BY U.Name ";

        var adapter = new SQLiteDataAdapter(sql, connection);

        if (string.IsNullOrEmpty(partGUID) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@PartGUID", partGUID);

        if (string.IsNullOrEmpty(name) == false)
          adapter.SelectCommand.Parameters.AddWithValue("@Name", name);

        adapter.Fill(dt);
      }

      List<SUser> userList = new List<SUser>();

      foreach (DataRow row in dt.Rows)
      {
        SUser user = new SUser();
        user.GUID = Convert.ToString(row["GUID"]);
        user.PartGUID = Convert.ToString(row["PartGUID"]);
        user.PartName = Convert.ToString(row["PartName"]);
        user.Name = Convert.ToString(row["Name"]);
        user.Password = Convert.ToString(row["Password"]);
        user.Role = Convert.ToString(row["Role"]);
        user.ViewOnly = SCommon.GetBoolean(row["ViewOnly"]);
        user.Description = Convert.ToString(row["Description"]);
        user.Width = Convert.ToInt32(row["Width"]);
        user.Height = Convert.ToInt32(row["Height"]);
        user.SplitterDistance = Convert.ToInt32(row["SplitterDistance"]);
        user.TreeFont = Convert.ToString(row["TreeFont"]);
        user.TreeFontSize = Convert.ToString(row["TreeFontSize"]);
        user.ExpandType = Convert.ToString(row["ExpandType"]);
        user.ShowID = SCommon.GetBoolean(row["ShowID"]);
        user.ShowEng = SCommon.GetBoolean(row["ShowEng"]);
        user.SortType = Convert.ToString(row["SortType"]);
        user.MonospacedFontDAOperationDesc = SCommon.GetBoolean(row["MonospacedFontDAOperationDesc"]);

        user.RegisteredDate = Convert.ToString(row["RegisteredDate"]);
        user.RegisteredPartGUID = Convert.ToString(row["RegisteredPartGUID"]);
        user.RegisteredPartName = Convert.ToString(row["RegisteredPartName"]);
        user.RegisteredUserGUID = Convert.ToString(row["RegisteredUserGUID"]);
        user.RegisteredUserName = Convert.ToString(row["RegisteredUserName"]);

        user.LastModifiedDate = Convert.ToString(row["LastModifiedDate"]);
        user.LastModifiedPartGUID = Convert.ToString(row["LastModifiedPartGUID"]);
        user.LastModifiedPartName = Convert.ToString(row["LastModifiedPartName"]);
        user.LastModifiedUserGUID = Convert.ToString(row["LastModifiedUserGUID"]);
        user.LastModifiedUserName = Convert.ToString(row["LastModifiedUserName"]);

        userList.Add(user);
      }

      return userList;
    }

    public void InsertUser(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " INSERT INTO TB_USER " +  
          " VALUES " + 
          " ( " +
          "   @GUID " + 
          " , @PartGUID " + 
          " , @Name " +
          " , @Password " +
          " , @Role " +
          " , @ViewOnly " +
          " , @Description " +
          " , @Width " + 
          " , @Height " + 
          " , @SplitterDistance " + 
          " , @TreeFont " + 
          " , @TreeFontSize " + 
          " , @ExpandType " + 
          " , @ShowID " + 
          " , @ShowEng " + 
          " , @SortType " + 
          " , @MonospacedFontDAOperationDesc " + 
          " , @RegisteredDate, @RegisteredPartGUID, @RegisteredUserGUID, @LastModifiedDate, @LastModifiedPartGUID, @LastModifiedUserGUID " +
          " )";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        cmd.Parameters.AddWithValue("@PartGUID", user.PartGUID);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@Role", user.Role);
        cmd.Parameters.AddWithValue("@ViewOnly", user.ViewOnly);
        cmd.Parameters.AddWithValue("@Description", user.Description);
        cmd.Parameters.AddWithValue("@Width", SCommon.DEFAULT_FORM_WIDTH);
        cmd.Parameters.AddWithValue("@Height", SCommon.DEFAULT_FORM_HEIGHT);
        cmd.Parameters.AddWithValue("@SplitterDistance", SCommon.DEFAULT_SPLITTER_DISTANCE);
        cmd.Parameters.AddWithValue("@TreeFont", "맑은 고딕");
        cmd.Parameters.AddWithValue("@TreeFontSize", "9.75");
        cmd.Parameters.AddWithValue("@ExpandType", "BizPackage");
        cmd.Parameters.AddWithValue("@ShowID", "0");
        cmd.Parameters.AddWithValue("@ShowEng", "0");
        cmd.Parameters.AddWithValue("@SortType", "Name");
        cmd.Parameters.AddWithValue("@MonospacedFontDAOperationDesc", "0");
        this.SetRegistered(cmd);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUserMyOption(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_USER " + 
          "    SET Width = @Width " +
          "      , Height = @Height " +
          "      , SplitterDistance = @SplitterDistance " +
          "      , ExpandType = @ExpandType " +
          "      , ShowID = @ShowID " +
          "      , ShowEng = @ShowEng " +
          "      , SortType = @SortType " +
          "      , LastModifiedDate = @LastModifiedDate " +
          "      , LastModifiedPartGUID = @LastModifiedPartGUID " +
          "      , LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Width", user.Width);
        cmd.Parameters.AddWithValue("@Height", user.Height);
        cmd.Parameters.AddWithValue("@SplitterDistance", user.SplitterDistance);
        cmd.Parameters.AddWithValue("@ExpandType", user.ExpandType);
        cmd.Parameters.AddWithValue("@ShowID", user.ShowID);
        cmd.Parameters.AddWithValue("@ShowEng", user.ShowEng);
        cmd.Parameters.AddWithValue("@SortType", user.SortType);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUserTreeOption(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_USER " +
          "    SET TreeFont = @TreeFont " +
          "      , TreeFontSize = @TreeFontSize " +
          "      , LastModifiedDate = @LastModifiedDate " +
          "      , LastModifiedPartGUID = @LastModifiedPartGUID " +
          "      , LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@TreeFont", user.TreeFont);
        cmd.Parameters.AddWithValue("@TreeFontSize", user.TreeFontSize);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUserMonospacedFontDAOperationDesc(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_USER " +
          "    SET MonospacedFontDAOperationDesc = @MonospacedFontDAOperationDesc " +
          "      , LastModifiedDate = @LastModifiedDate " +
          "      , LastModifiedPartGUID = @LastModifiedPartGUID " +
          "      , LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID ";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@MonospacedFontDAOperationDesc", user.MonospacedFontDAOperationDesc);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUserPassword(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          " UPDATE TB_USER " + 
          "    SET Password = @Password " +
          "      , LastModifiedDate = @LastModifiedDate " +
          "      , LastModifiedPartGUID = @LastModifiedPartGUID " +
          "      , LastModifiedUserGUID = @LastModifiedUserGUID " +
          "  WHERE GUID = @GUID";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void UpdateUser(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql =
          "UPDATE TB_USER SET " +
          "   PartGUID = @PartGUID " +
          " , Name = @Name " +
          " , Password = @Password " +
          " , Role = @Role " +
          " , ViewOnly = @ViewOnly " +
          " , Description = @Description " +
          " , LastModifiedDate = @LastModifiedDate " +
          " , LastModifiedPartGUID = @LastModifiedPartGUID " +
          " , LastModifiedUserGUID = @LastModifiedUserGUID " +
          " WHERE GUID = @GUID";

        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@PartGUID", user.PartGUID);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@Role", user.Role);
        cmd.Parameters.AddWithValue("@ViewOnly", user.ViewOnly);
        cmd.Parameters.AddWithValue("@Description", user.Description);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        this.SetLastModified(cmd);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    public void DeleteUser(SUser user)
    {
      using (SQLiteConnection connection = new SQLiteConnection(this.ConnectionString))
      {
        connection.Open();

        string sql = "DELETE FROM TB_USER WHERE GUID = @GUID";
        SQLiteCommand cmd = new SQLiteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@GUID", user.GUID);
        cmd.ExecuteNonQuery();

        connection.Close();
      }
    }

    #endregion


    #region Registered, Last Modified

    private void SetRegistered(SQLiteCommand cmd, SComponent component)
    {
      cmd.Parameters.AddWithValue("@RegisteredDate", component.RegisteredDate);
      cmd.Parameters.AddWithValue("@RegisteredPartGUID", component.RegisteredPartGUID);
      cmd.Parameters.AddWithValue("@RegisteredUserGUID", component.RegisteredUserGUID);
    }

    private void SetLastModified(SQLiteCommand cmd, SComponent component)
    {
      cmd.Parameters.AddWithValue("@LastModifiedDate", component.LastModifiedDate);
      cmd.Parameters.AddWithValue("@LastModifiedPartGUID", component.LastModifiedPartGUID);
      cmd.Parameters.AddWithValue("@LastModifiedUserGUID", component.LastModifiedUserGUID);
    }

    private void SetRegistered(SQLiteCommand cmd)
    {
      cmd.Parameters.AddWithValue("@RegisteredDate", SCommon.GetDate());
      cmd.Parameters.AddWithValue("@RegisteredPartGUID", SCommon.LoggedInUser.PartGUID);
      cmd.Parameters.AddWithValue("@RegisteredUserGUID", SCommon.LoggedInUser.GUID);
    }

    private void SetLastModified(SQLiteCommand cmd)
    {
      cmd.Parameters.AddWithValue("@LastModifiedDate", SCommon.GetDate());
      cmd.Parameters.AddWithValue("@LastModifiedPartGUID", SCommon.LoggedInUser.PartGUID);
      cmd.Parameters.AddWithValue("@LastModifiedUserGUID", SCommon.LoggedInUser.GUID);
    }

    #endregion
  }
}